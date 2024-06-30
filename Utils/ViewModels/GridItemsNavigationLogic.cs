using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Items;
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
    public abstract class GridItemsNavigationLogic: GridPaginationLogic<Item>
    { 
        #region Properties
        // used to pass data between view models as channels and listeners
        private readonly IEventAggregator _eventAggregator;
        // used to navigate back 
        private readonly List<Item> _currentPath = [];
        // used for database operations getting categories and products
        private readonly IItemsRepo _itemsRepo;

        private int _currentFolderId;
        public int CurrentFolderId
        {
            get => _currentFolderId;
            set { _currentFolderId = value; OnPropertyChanged(); OpenFolder(); }
        }

        // items list
        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            set { 
                _items = value; 
                OnPropertyChanged(); 
                OnAllItemsChanged(value);
                /*ItemsUpdated(value);*/
            }
        }

        /// <summary>
        /// also refered as categories
        /// </summary>
        private ObservableCollection<Item> _folders;
        // folders list
        public ObservableCollection<Item> Folders
        {
            get => _folders;
            set { _folders = value; OnPropertyChanged(); /*FoldersUpdated(value);*/ }
        }


        #endregion

        public ICommand ItemClickCommand { get; set; }
        public ICommand OnBackClickedCommand { get; set; }


        // constructor with dependancy injection
        public GridItemsNavigationLogic(
            IItemsRepo itemsRepo,
            IEventAggregator eventAggregator)
        {
            _itemsRepo = itemsRepo;

            // just initialize them
            _items = [];
            _folders = [];
            // geting them realy
            CurrentFolderId = 0;

            // initiate commands here
            ItemClickCommand = new ClickCommand<Item>((x) => OnItemClicked(x));
            OnBackClickedCommand = new FunCommand(OnGoBackClicked);

            // gets the current emploee passed from the HomeVM 
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<EditItemDatabaseUpdatedChannel>().Subscribe(OnDatabaseChanged);
        }

        // listen to database changes and refresh 
        private void OnDatabaseChanged() => OpenFolder();


        #region navigation methods
        // adds item to the invoice or open the item (if IsFolder)
        private void OnItemClicked(Item item)
        {
            if (item.IsFolder)
            {
                // open folder by changing the _currentFolderId
                CurrentFolderId = item.Id;
                _currentPath.Add(item);
            }

            ItemSelected(item);
        }


        /// <summary>
        /// goes to the previous parent category (obviosly if the current category (folder) 
        /// is a sub-category of another
        /// </summary>
        private void OnGoBackClicked()
        {
            if (_currentPath.Count != 0)
            {
                // get the current folder
                Item currentFolder = _currentPath.Last();
                // remove it from the path
                _currentPath.Remove(currentFolder);
                // change the current folder id to be it's parent id
                CurrentFolderId = currentFolder.ParentId;

                // TODO Update UpdateCurrentItem
            }
        }


        /// <summary>
        /// when category is chosen it gets all the products in the category or open
        /// a list of all the sub categories 
        /// </summary>
        private async void OpenFolder()
        {
            List<Item> items = await _itemsRepo.GetItems(CurrentFolderId);
            Items = new ObservableCollection<Item>(items);

            if (items.Count > 0) return;

            List<Item> folders = await _itemsRepo.GetFolders(CurrentFolderId);
            if (folders.Count != 0)
            {
                Folders = new ObservableCollection<Item>(folders);
            }
        }




        #endregion

        #region Items Grid Arrangement
        // arrange the products grid to rows and columns
        // these functions are inhereted from the GridPaginationLogic Class
        public override int GetRows() => 3;
        public override int GetColumns() => 4;

        // For the catecories list grid
        public static int RowsOfFolders => 2;
        public static int ColumnsOfFolders => 4;
        #endregion

        /// <summary>
        /// it notify all classes that implement that class of products changes when the category changes
        /// </summary>
        /// <param name="item">the products in the current category</param>
        public abstract void ItemSelected(Item item);

    }
}
