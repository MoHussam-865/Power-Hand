using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Power_Hand.Data;
using Power_Hand.Data.Repository.Invoices;
using Power_Hand.Data.Repository.Items;
using Power_Hand.DBContext;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.ViewModels
{
    class CasherVM: ViewModel
    {
        // used to pass data between view models
        private readonly IEventAggregator _eventAggregator;
        // the current signed casher used to be added to the invoice
        private Emploee? _currentEmploee;
        // used for navigation between viewmodels (views)
        private INavigationService _navigationService;
        public INavigationService MyNavigationService { 
            get => _navigationService;
            set 
            { 
                _navigationService = value; 
                OnPropertyChanged(); 
            } 
        }
        
        // used for database operations
        private readonly IItemsRepo _itemsRepo;
        private readonly IInvoicesRepo _invoicesRepo;

        // 
        private int _currentFolderId = 0;
        public int CurrentFolderId
        {
            get => _currentFolderId; 
            set 
            {
                _currentFolderId = value;
                OnPropertyChanged();
                OpenFolder();
            }
        }

        private double _currentQty;
        public double CurrentQty
        {
            get => _currentQty; 
            set 
            {
                _currentQty = value;
                OnPropertyChanged();
            }
        }

        private InvoiceItem? _selectedItem;
        public InvoiceItem? SelectedItem
        {
            get =>  _selectedItem;
            set 
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            set 
            {
                _items = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Item> _folders;
        public ObservableCollection<Item> Folders
        {
            get => _folders;
            set
            {
                _folders = value;
                OnPropertyChanged();
            }
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
            _folders = new ObservableCollection<Item>();
            _items = new ObservableCollection<Item>();
            // geting them realy
            OpenFolder();
    
            // no invoice items yet
            _invoiceItems = new ObservableCollection<InvoiceItem>();

            // gets the current emploee passed from the HomeVM 
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<EmploeeShare>().Subscribe(OnEmploeeSigned);

        }

        private async void OpenFolder()
        {
            List<Item> items = await _itemsRepo.GetItems(CurrentFolderId);
            Items = new ObservableCollection<Item>(items);

            List<Item> folders = await _itemsRepo.GetFolders(CurrentFolderId);
            Folders = new ObservableCollection<Item>(folders);
        }


        private void OnEmploeeSigned(Emploee emploee) => _currentEmploee = emploee;

        private void OnItemClicked(Item item)
        {
            // convert to invoice item
            InvoiceItem myItem = item.ToInvoiceItem();
            // add item to _invoiceItems
            InvoiceItems.Add(myItem);
        }


        private void OnFolderClicked(Item item)
        {
            // open folder by changing the _currentFolderId

        }

        private void OnItemDelete(InvoiceItem item)
        {
            // remove from _invoiceItems
            InvoiceItems.Remove(item);
        }

        private void OnItemEdit(InvoiceItem item)
        {
            // first set the ui to get the new qty
            
            // get the updated item and add it to the invoice items and remove the old one
        }

        private void OnQuantityChanged()
        {
            
        }













    }
}
