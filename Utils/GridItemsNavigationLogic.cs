using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Data.SharedData;
using Prism.Events;
using System.Windows.Input;
using Power_Hand.Models;
using Power_Hand.Interfaces;

namespace Power_Hand.Utils
{
    public abstract class GridItemsNavigationLogic : ViewModel
    {
        #region Properties
        // used to pass data between view models
        private readonly IEventAggregator _eventAggregator;
        // used to navigate back 
        private readonly List<Item> _currentPath = [];
        // used for database operations
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
            set { _items = value; OnPropertyChanged(); /*ItemsUpdated(value);*/ }
        }

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
            _eventAggregator.GetEvent<ItemDatabaseUpdated>().Subscribe(OnDatabaseChanged);
        }

        // refresh the data
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


        // goes to the previous folder
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

        // gets folder content 
        private async void OpenFolder()
        {
            List<Item> items = await _itemsRepo.GetItems(CurrentFolderId);
            Items = new ObservableCollection<Item>(items);

            List<Item> folders = await _itemsRepo.GetFolders(CurrentFolderId);
            if (folders.Count != 0)
            {
                Folders = new ObservableCollection<Item>(folders);
            }
        }

        #endregion



        public abstract void ItemSelected(Item item);

        //public abstract void FoldersUpdated(ObservableCollection<Item> folders);

        //public abstract void ItemsUpdated(ObservableCollection<Item> items);

    }
}
