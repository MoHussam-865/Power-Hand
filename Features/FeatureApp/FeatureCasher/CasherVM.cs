using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Invoices;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Data.SharedData;
using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureCasher
{
    class CasherVM : ViewModel
    {
        // used to pass data between view models
        private readonly IEventAggregator _eventAggregator;
        // the current signed casher used to be added to the invoice
        private Emploee? _currentEmploee;
        // 
        private readonly IInvoicesRepo _invoicesRepo;
        //
        private readonly SharedValuesStore _appStore;

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
            set
            {
                _invoiceItems = value;
                OnPropertyChanged();
            }
        }

        private CasherItemsNavigationVM _gridItemsVM;
        public CasherItemsNavigationVM GridItemsVM
        {
            get => _gridItemsVM;
            set { _gridItemsVM = value; OnPropertyChanged(); }
        }

        private InvoiceItemsListVM _invoiceItemsListVM;
        public InvoiceItemsListVM InvoiceItemsListVM
        {
            get => _invoiceItemsListVM;
            set { _invoiceItemsListVM = value; OnPropertyChanged(); }
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
            CasherItemsNavigationVM gridItemsVM,
            InvoiceItemsListVM invoiceItemsListVM,
            NavigationBarVM navigationBarVM,
            SharedValuesStore store)
        {
            _gridItemsVM = gridItemsVM;
            _invoiceItemsListVM = invoiceItemsListVM;
            _invoicesRepo = invoicesRepo;
            _navigationVM = navigationBarVM;
            _appStore = store;
            _currentEmploee = _appStore.Emploee;

            // no invoice items yet
            _invoiceItems = [];


            ItemRemoveCommand = new FunCommand(OnItemDelete);
            ItemEditCommand = new FunCommand(OnItemEdit);
            QtyChangeCommand = new FunCommand(OnQuantityChanged);
            DiscardCommand = new FunCommand(OnDiscard);
            SaveInvoiceCommand = new FunCommand(OnSaveInvoice);

            // gets the current emploee passed from the HomeVM 
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<EmploeeShare>().Subscribe(OnEmploeeSelected);
            _eventAggregator.GetEvent<CasherInvoiceItemsListChannel>().Subscribe(OnInvoiceItemsChanged);
            _eventAggregator.GetEvent<SelectedInvoiceItemShareChannel>().Subscribe(OnSelectedItemChanges);
        }

        private void OnEmploeeSelected(Emploee emploee)
        {
            _currentEmploee = emploee;
        }

        private void OnSelectedItemChanges(InvoiceItem? item)
        {
            _selectedItem = item;
        }

        private void OnInvoiceItemsChanged(ObservableCollection<InvoiceItem> list)
        {
            InvoiceItems = list;
        }


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

                Invoice myInvoice = new(
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
            _eventAggregator.GetEvent<CasherInvoiceItemsListChannel>().Publish(InvoiceItems);
            _eventAggregator.GetEvent<SelectedInvoiceItemShareChannel>().Publish(_selectedItem);
        }

    }
}
