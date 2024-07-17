using System.Windows.Controls;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;
using Power_Hand.Models;
using Power_Hand.Utils.ViewModels;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureCasher
{
    /// <summary>
    /// inherit from GridItemsNavigationLogic which gets products from different categories
    /// and it inherit from GridPaginationLogic Responsable for pagination between pages of items
    /// in the same category
    /// 
    /// </summary>
    public class CasherItemsNavigationVM(
        IItemsRepo itemsRepo,
        IEventAggregator eventAggregator) : GridItemsNavigationLogic(itemsRepo, eventAggregator)
    {
        private readonly IEventAggregator _eventAggregator = eventAggregator;

        /// <summary>
        /// the GridItemsNavigationLogic abstract class handle the current products (items) in the 
        /// category via this method
        /// </summary>
        public override void ItemSelected(Item item)
        {
            if (item.IsFolder)
            {
                _eventAggregator.GetEvent<CasherFolderShareChannel>().Publish(item);
            }
            _eventAggregator.GetEvent<CasherItemListChannel>().Publish(!item.IsFolder? item: null);
        }

       
    }
}
