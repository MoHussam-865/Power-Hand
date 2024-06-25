using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Data.SharedData;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Power_Hand.Utils;
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
                _eventAggregator.GetEvent<FolderShare>().Publish(item);
            }
            _eventAggregator.GetEvent<ItemShare>().Publish(!item.IsFolder? item: null);
        }

        
    }
}
