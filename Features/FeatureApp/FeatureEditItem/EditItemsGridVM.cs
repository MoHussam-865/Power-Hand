using System.Windows.Controls;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Features.FeatureApp.FeatureEditItem.Channels;
using Power_Hand.Models;
using Power_Hand.Utils.ViewModels;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureEditItem
{
    public class EditItemsGridVM(IItemsRepo itemsRepo, IEventAggregator eventAggregator) : GridItemsNavigationLogic(itemsRepo, eventAggregator)
    {
        private readonly IEventAggregator _eventAggregator = eventAggregator;

        

        public override void ItemSelected(Item item)
        {
            if (item.IsFolder)
            {
                _eventAggregator.GetEvent<EditItemCurrentFolderShareChannel>().Publish(item);
                return;
            }
            _eventAggregator.GetEvent<EditSelectedItemShareChannel>().Publish(item);
        }


    }
}
