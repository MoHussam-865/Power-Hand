using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;
using Power_Hand.Features.FeatureApp.FeatureEditItem.Channels;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;

// Note ToDo keep in mind that categories that containes sub-categories (folders) cannot have products
// in them directly
namespace Power_Hand.Utils.ViewModels
{
    /// <summary>
    /// this is an abstract class that encapsulate the logic of navigation between categories of products
    /// it also implement the GridPaginationLogic (abstract class) that handle the pagination between pages in the 
    /// same category if the number of products in the list is more than the grid (display the products) can fit
    /// 
    /// it keeps track of the current category (also refaered as folder) and gets all its content
    /// </summary>
    public class GridItemsNavigationLogic : MiddleItemFolderLogic
    {
        #region Properties
        // used to pass data between view models as channels and listeners
        private readonly IEventAggregator _eventAggregator;
        // used for database operations getting categories and products
        private readonly IItemsRepo _itemsRepo;


        // items list
        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
                RefreshPage(value);
            }
        }


        #endregion

        // constructor with dependancy injection
        public GridItemsNavigationLogic(
            IItemsRepo itemsRepo,
            IEventAggregator eventAggregator): base(eventAggregator) {
            _itemsRepo = itemsRepo;

            // just initialize them
            _items = [];

            // gets the current employee passed from the HomeVM 
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<FolderIdChangedChannel>().Subscribe(OnFolderOpened);
        }



        private async void OnFolderOpened(int folderId)
        {
            List<Item> items = await _itemsRepo.GetItems(folderId);
            Items = new ObservableCollection<Item>(items);
        }


        #region Items Grid Arrangement


        public override void OnItemClicked(Item item)
        {
            throw new NotImplementedException();
        }


        public override int MyRows() => 3;

        public override int MyColums() => 4;
        #endregion


    }
}
