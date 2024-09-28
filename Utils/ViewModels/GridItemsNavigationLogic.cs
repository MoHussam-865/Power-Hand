using System.Collections.ObjectModel;
using MyDatabase.Models;
using MyDatabase.Repository.Items;
using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;
using Power_Hand.Features.FeatureApp.FeatureEditItem.Channels;


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
        private int _currentFolder = 0;


        // items list
        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                RefreshPage(value);
                OnPropertyChanged();
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
            _eventAggregator.GetEvent<FolderChangedChannel>().Subscribe(OnFolderOpened);
        }



        private async void OnFolderOpened(int folderId)
        {
            _currentFolder = folderId;
            List<Item> items = await _itemsRepo.GetItems(folderId);
            Items = new ObservableCollection<Item>(items);
        }


        #region Items Grid Arrangement


        public override void OnItemClicked(Item item)
        {
            _eventAggregator.GetEvent<EditSelectedItemShareChannel>().Publish(item);
            _eventAggregator.GetEvent<CasherItemListChannel>().Publish(item);
        }


        public override int MyRows() => 3;

        public override int MyColums() => 4;

        public override ObservableCollection<Item> OnDatabaseChanged()
        {
            OnFolderOpened(_currentFolder);
            return Items;
        }
        #endregion


    }
}
