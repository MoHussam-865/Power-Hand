using System.Collections.ObjectModel;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Data.SharedData;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.ViewModels
{
    public class GridItems_SVM : ViewModel
    {

        // used to pass data between view models
        private readonly IEventAggregator _eventAggregator;

        // used to navigate back 
        private List<Item> _currentPath = [];
        // used for database operations
        private readonly IItemsRepo _itemsRepo;


        // 
        private int _currentFolderId;
        public int CurrentFolderId
        {
            get => _currentFolderId;
            set { _currentFolderId = value; OnPropertyChanged(); OpenFolder(); }
        }

        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            set { _items = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Item> _folders;
        public ObservableCollection<Item> Folders
        {
            get => _folders;
            set { _folders = value; OnPropertyChanged(); }
        }

        private ObservableCollection<InvoiceItem> _invoiceItems;
        public ObservableCollection<InvoiceItem> InvoiceItems
        {
            get => _invoiceItems;
            set
            {
                _invoiceItems = value;
                OnPropertyChanged();
            }
        }


        public ICommand ItemClickCommand { get; set; }
        public ICommand OnBackClickedCommand { get; set; }


        // constructor with dependancy injection
        public GridItems_SVM(
            IItemsRepo itemsRepo,
            IEventAggregator eventAggregator)
        {
            _itemsRepo = itemsRepo;

            // just initialize them
            _folders = [];
            _items = [];
            // geting them realy
            CurrentFolderId = 0;

            // no invoice items yet
            _invoiceItems = [];

            // initiate commands here
            ItemClickCommand = new ClickCommand<Item>((x) => OnItemClicked(x));
            OnBackClickedCommand = new FunCommand(OnGoBackClicked);

            // gets the current emploee passed from the HomeVM 
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<InvoiceItemsShare>().Subscribe(OnInvoiceItemsChange);
            _eventAggregator.GetEvent<ItemDatabaseUpdated>().Subscribe(OnDatabaseChanged);
        }

        private void OnDatabaseChanged() => OpenFolder();


        private void OnInvoiceItemsChange(ObservableCollection<InvoiceItem> list)
        {
            InvoiceItems = list;
        }


        // adds item to the invoice or open the item (if IsFolder)
        private void OnItemClicked(Item item)
        {
            if (item.IsFolder)
            {
                // open folder by changing the _currentFolderId
                CurrentFolderId = item.Id;
                _currentPath.Add(item);
                _eventAggregator.GetEvent<ItemShare>().Publish(null);
            }
            else
            {
                // convert to invoice item
                InvoiceItem myItem = item.ToInvoiceItem();
                // add item to _invoiceItems
                InvoiceItems.Add(myItem);
                _eventAggregator.GetEvent<InvoiceItemsShare>().Publish(InvoiceItems);

            }
            UpdateCurrentItem(item);
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

        private void UpdateCurrentItem(Item item)
        {
            if (item.IsFolder)
            {
                _eventAggregator.GetEvent<FolderShare>().Publish(item);
                _eventAggregator.GetEvent<ItemShare>().Publish(null);
            }
            else
            {
                _eventAggregator.GetEvent<ItemShare>().Publish(item);
                _eventAggregator.GetEvent<FolderShare>().Publish(null);
            }
        }
    }
}
