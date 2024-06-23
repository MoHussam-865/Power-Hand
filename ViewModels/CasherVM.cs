using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Invoices;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Data.SharedData;
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
         
        private readonly IInvoicesRepo _invoicesRepo;

        // 
        private NavigationBarVM _navigationVM;
        public NavigationBarVM NavigationVM
        {
            get => _navigationVM;
            set { _navigationVM = value; OnPropertyChanged(); }
        }
        

        private double _currentQty;
        public double CurrentQty
        {
            get => _currentQty;
            set { _currentQty = value; OnPropertyChanged(); }
        }

        private InvoiceItem? _selectedItem;

        private ObservableCollection<InvoiceItem> _invoiceItems;
        public ObservableCollection<InvoiceItem> InvoiceItems
        {
            get => _invoiceItems;
            set { 
                _invoiceItems = value; 
                OnPropertyChanged(); 
            }
        }

        private GridItems_SVM _gridItemsVM;
        public GridItems_SVM GridItemsVM
        {
            get => _gridItemsVM; 
            set { _gridItemsVM = value; OnPropertyChanged(); }
        }

        private InvoiceItemsList_SVM _invoiceListVM;
        public InvoiceItemsList_SVM InvoiceListVM
        {
            get => _invoiceListVM;
            set { _invoiceListVM = value; OnPropertyChanged(); }
        }



        public ICommand ItemEditCommand { get; set; }
        public ICommand ItemRemoveCommand { get; set; }
        public ICommand DiscardCommand { get; set; }
        public ICommand QtyChangeCommand { get; set; }
        public ICommand SaveInvoiceCommand { get; set; }
        

        // constructor with dependancy injection
        public CasherVM(
            IInvoicesRepo invoicesRepo,
            IEventAggregator eventAggregator,
            GridItems_SVM gridItems_SVM,
            InvoiceItemsList_SVM invoiceItemsList_SVM,
            NavigationBarVM navigationBarVM)
        {
            _gridItemsVM = gridItems_SVM;
            _invoiceListVM = invoiceItemsList_SVM;
            _invoicesRepo = invoicesRepo;
            _navigationVM = navigationBarVM;

            // no invoice items yet
            _invoiceItems = [];


            ItemRemoveCommand = new FunCommand(OnItemDelete);
            ItemEditCommand = new FunCommand(OnItemEdit);
            QtyChangeCommand = new FunCommand(OnQuantityChanged);
            DiscardCommand = new FunCommand(OnDiscard);
            SaveInvoiceCommand = new FunCommand(OnSaveInvoice);
            
            // gets the current emploee passed from the HomeVM 
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<EmploeeShare>().Subscribe(OnEmploeeSigned);
            _eventAggregator.GetEvent<InvoiceItemsShare>().Subscribe(OnInvoiceItemsChanged);
            _eventAggregator.GetEvent<SelectedInvoiceItemShare>().Subscribe(OnSelectedItemChanges);
        }

        private void OnSelectedItemChanges(InvoiceItem? item)
        {
            _selectedItem = item;
        }

        private void OnInvoiceItemsChanged(ObservableCollection<InvoiceItem> list)
        {
            InvoiceItems = list;
        }



        // gets the passed emploee from the HomeVM.cs (Home view model)
        private void OnEmploeeSigned(Emploee emploee) => _currentEmploee = emploee;

        private void OnItemDelete()
        {
            if (_selectedItem != null)
            {
                InvoiceItems.Remove(_selectedItem);
                _selectedItem = null;

                Update();
            }
        }

        private void OnItemEdit()
        {
            // first set the ui to get the new qty

            // get the updated item and add it to the invoice items and remove the old one


            Update();
        }


        private void OnDiscard()
        {
            InvoiceItems.Clear();
            _selectedItem = null;
            Update();
        }
        private void OnQuantityChanged()
        {
            // Not implemented yet

            Update();
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
                _selectedItem = null;
                Update();
            }
            else
            {

                Debug.WriteLine(_currentEmploee?.Id.ToString());
            }
        }

        private void Update()
        {
            _eventAggregator.GetEvent<InvoiceItemsShare>().Publish(InvoiceItems);
            _eventAggregator.GetEvent<SelectedInvoiceItemShare>().Publish(_selectedItem);
        }

    }
}
