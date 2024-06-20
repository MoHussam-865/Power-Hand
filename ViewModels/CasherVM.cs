using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Power_Hand.Commands;
using Power_Hand.Data;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Invoices;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.ViewModels
{
    class CasherVM : ViewModel
    {
        // used to pass data between view models
        private readonly IEventAggregator _eventAggregator;
        // the current signed casher used to be added to the invoice
        private Emploee? _currentEmploee;
        // used for navigation between viewmodels (views)
        private INavigationService _navigationService;
        public INavigationService MyNavigationService
        {
            get => _navigationService;
            set { _navigationService = value; OnPropertyChanged(); }
        }

        // used for database operations
        private readonly IItemsRepo _itemsRepo;
        private readonly IInvoicesRepo _invoicesRepo;

        // 
        private int _currentFolderId = 0;
        public int CurrentFolderId
        {
            get => _currentFolderId;
            set { _currentFolderId = value; OnPropertyChanged(); OpenFolder(); }
        }

        private double _currentQty;
        public double CurrentQty
        {
            get => _currentQty;
            set { _currentQty = value; OnPropertyChanged(); }
        }

        private InvoiceItem? _selectedItem;
        public InvoiceItem? SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); }
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
            set { _invoiceItems = value; OnPropertyChanged(); }
        }


        public ICommand ItemClickCommand { get; set; }
        public ICommand ItemEditCommand { get; set; }
        public ICommand ItemRemoveCommand { get; set; }
        public ICommand DiscardCommand { get; set; }
        public ICommand ItemSelectCommand { get; set; }
        public ICommand QtyChangeCommand { get; set; }
        public ICommand SaveInvoiceCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand ToReservastionCommand { get; set; }


        // constructor with dependancy injection
        public CasherVM(
            IItemsRepo itemsRepo,
            IInvoicesRepo invoicesRepo,
            INavigationService navigationService,
            IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _itemsRepo = itemsRepo;
            _invoicesRepo = invoicesRepo;

            // just initialize them
            _folders = [];
            _items = [];
            // geting them realy
            OpenFolder();

            // no invoice items yet
            _invoiceItems = [];

            // initiate commands here
            ItemClickCommand = new ClickCommand<Item>((x) => OnItemClicked(x));
            ItemSelectCommand = new ClickCommand<InvoiceItem>((x) => OnItemSelected(x));


            ItemRemoveCommand = new FunCommand(OnItemDelete);
            ItemEditCommand = new FunCommand(OnItemEdit);
            QtyChangeCommand = new FunCommand(OnQuantityChanged);
            DiscardCommand = new FunCommand(OnDiscard);
            SaveInvoiceCommand = new FunCommand(OnSaveInvoice);
            LogOutCommand = new FunCommand(OnLogOutClicked);
            ToReservastionCommand = new FunCommand(OnNavigateToReservationClicked);

            // gets the current emploee passed from the HomeVM 
            _eventAggregator = eventAggregator;

            
            _eventAggregator.GetEvent<EmploeeShare>().Subscribe(OnEmploeeSigned);
        }

        private void OnLogOutClicked() => MyNavigationService.NavigateTo<HomeVM>();

        private void OnNavigateToReservationClicked() => MyNavigationService.NavigateTo<ReservationVM>();

        private void OnItemSelected(InvoiceItem item) => SelectedItem = item;


        private void OnDiscard() => InvoiceItems.Clear();

        private async void OpenFolder()
        {
            List<Item> items = await _itemsRepo.GetItems(CurrentFolderId);
            if (items.Count != 0)
            {
                Items = new ObservableCollection<Item>(items);
            }

            List<Item> folders = await _itemsRepo.GetFolders(CurrentFolderId);
            if (folders.Count != 0)
            {
                Folders = new ObservableCollection<Item>(folders);
            }
        }


        private void OnEmploeeSigned(Emploee emploee) => _currentEmploee = emploee;

        private void OnItemClicked(Item item)
        {
            if (item.IsFolder)
            {
                // open folder by changing the _currentFolderId
                CurrentFolderId = item.Id;
            }
            else
            {
                // convert to invoice item
                InvoiceItem myItem = item.ToInvoiceItem();
                // add item to _invoiceItems
                InvoiceItems.Add(myItem);
            }
        }


        private void OnItemDelete()
        {
            if (_selectedItem != null)
            {
                InvoiceItems.Remove(_selectedItem);
            }
        }

        private void OnItemEdit()
        {
            // first set the ui to get the new qty

            // get the updated item and add it to the invoice items and remove the old one
        }

        private void OnQuantityChanged()
        {

        }

        private void OnSaveInvoice()
        {

            if (InvoiceItems.Count > 0 && _currentEmploee != null)
            {

                Invoice myInvoice = new Invoice(
                    date: DateTime.Now.Ticks,
                    type: 0,
                    emploeeId: _currentEmploee.Id,
                    payed: InvoiceItems.ToList().Sum(i => i.Total),
                    remaining: 0,
                    items: [.. InvoiceItems]  // this makes it a list => InvoiceItems.ToList()
                    );

                _invoicesRepo.AddInvoice(myInvoice);
                InvoiceItems.Clear();
            }
            else
            {

                Debug.WriteLine(_currentEmploee?.Id.ToString());
            }
        }















    }
}
