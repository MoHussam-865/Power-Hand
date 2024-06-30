using System.Windows.Controls;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;
using Power_Hand.Models;
using Power_Hand.Utils.ViewModels;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureCasher
{
    public class CasherItemsNavigationVM(
        IItemsRepo itemsRepo,
        IEventAggregator eventAggregator) : GridItemsNavigationLogic(itemsRepo, eventAggregator)
    {
        
        private readonly IEventAggregator _eventAggregator = eventAggregator;


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
