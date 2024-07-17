using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Invoices;
using Power_Hand.Data.SharedData;
using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;
using Power_Hand.Features.Popups;
using Power_Hand.Features.Popups.ViewModels;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Power_Hand.Utils.ViewModels;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureCasher
{
    class CasherVM : ViewModel
    {
        // used to pass data between view models
        private readonly IEventAggregator _eventAggregator;
        // the current signed casher used to be added to the invoice
        private Employee? _currentEmployee;
        // 
        private readonly IInvoicesRepo _invoicesRepo;
        //
        private readonly SharedValuesStore _appStore;
        //
        private readonly INavigationService _navigationService;
        // calculator visibility
        private Visibility _calculatorVisibility;
        public Visibility CalculatorVisibility
        {
            get => _calculatorVisibility; 
            set { _calculatorVisibility = value; OnPropertyChanged(); }
        }

        private string _currentQty;
        public string CurrentQty
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

        #region some View models
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

        // calculator ViewModel
        private CalculatorVM _calculatorVM;

        public CalculatorVM CalculatorVM
        {
            get => _calculatorVM;
            set { _calculatorVM = value; OnPropertyChanged(); }
        }

        #endregion


        #region Command 
        public ICommand ItemEditCommand { get; set; }
        public ICommand ItemRemoveCommand { get; set; }
        public ICommand DiscardCommand { get; set; }
        public ICommand QtyChangeCommand { get; set; }
        public ICommand SaveInvoiceCommand { get; set; }
        public ICommand PhysicalKeyPressedCommand {  get; set; }
        #endregion

        // constructor with dependency injection
        public CasherVM(
            IInvoicesRepo invoicesRepo,
            IEventAggregator eventAggregator,
            INavigationService navigationService,
            CasherItemsNavigationVM gridItemsVM,
            InvoiceItemsListVM invoiceItemsListVM,
            CalculatorVM calculatorVM,
            SharedValuesStore store)
        {
            _calculatorVM = calculatorVM;
            _gridItemsVM = gridItemsVM;
            _invoiceItemsListVM = invoiceItemsListVM;
            _invoicesRepo = invoicesRepo;
            _navigationService = navigationService;
            _currentQty = "";
            _appStore = store;
            _currentEmployee = _appStore.Employee;
            _calculatorVisibility = Visibility.Collapsed;

            // no invoice items yet
            _invoiceItems = [];


            ItemRemoveCommand = new FunCommand(OnItemDelete);
            ItemEditCommand = new FunCommand(OnItemEdit);
            QtyChangeCommand = new FunCommand(OnQuantityChanged);
            DiscardCommand = new FunCommand(OnDiscard);
            SaveInvoiceCommand = new FunCommand(OnSaveInvoice);
            PhysicalKeyPressedCommand = new ClickCommand<KeyEventArgs>(OnPhysicalKeyPressed);


            // gets the current emploee passed from the HomeVM 
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<CasherInvoiceItemsListChannel>().Subscribe(OnInvoiceItemsChanged);
            _eventAggregator.GetEvent<SelectedInvoiceItemShareChannel>().Subscribe(OnSelectedItemChanges);
            _eventAggregator.GetEvent<ItemQuantityEditedChannel>().Subscribe(OnSelectedItemQuantityChanges);
            _eventAggregator.GetEvent<CalculatorVisibilityChannel>().Subscribe(OnCalculatorVisibilityChanged);
            _eventAggregator.GetEvent<RemoveInvoiceItemFromListChannel>().Subscribe(RemoveInvoiceItem);
        }

        private void RemoveInvoiceItem(InvoiceItem? item)
        {
            if (item == null)
            {
                _selectedItem = null;
            }
            else
            {
                InvoiceItems.Remove(item);
                _selectedItem = null;
            }
            Update();
        }

        private void OnPhysicalKeyPressed(KeyEventArgs e)
        {
            if (e.OriginalSource is TextBox textBox)
            {
                textBox.Text += e.Key;
            }

            _calculatorVM.OnPhysicalKeyPressed(e);
        }

        private void OnCalculatorVisibilityChanged(bool visible)
        {
            CalculatorVisibility = visible? Visibility.Visible: Visibility.Collapsed;
        }

        private void OnSelectedItemQuantityChanges(double quantity)
        {
            if (_selectedItem == null || quantity == 0) return;
            _selectedItem.Quantity = quantity;
            _eventAggregator.GetEvent<CasherInvoiceItemsListChannel>()
                .Publish(InvoiceItems);
            _eventAggregator.GetEvent<SelectedInvoiceItemShareChannel>().Publish(null);
        }


        // selected item for edit or delete
        private void OnSelectedItemChanges(InvoiceItem? item) => _selectedItem = item;
        

        // Updates the list of InvoiceItems 
        private void OnInvoiceItemsChanged(ObservableCollection<InvoiceItem> list) => InvoiceItems = list;
        
        private void OnItemDelete()
        {
            if (_selectedItem != null)
            {
                _appStore.InvoiceItemToDelete = _selectedItem;
                _navigationService.OpenPopup<DeleteInvoiceItemPopupVM>();
                _eventAggregator.GetEvent<PopupCloseChannel>().Publish(false);
            }
        }

        

        private void OnItemEdit()
        {
            if (_selectedItem == null) return;
            _navigationService.OpenPopup<EditInvoiceItemPopupVM>();
            _eventAggregator.GetEvent<PopupCloseChannel>().Publish(false);
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
            // the invoice must have items and casher 
            if (InvoiceItems.Count > 0 && _currentEmployee != null)
            {
                // create the invoice
                Invoice myInvoice = new(
                    date: DateTime.Now.Ticks,
                    type: 0,
                    employeeId: _currentEmployee.Id,
                    payed: InvoiceItems.ToList().Sum(i => i.Total),
                    remaining: 0,
                    items: [.. InvoiceItems]  // this makes it a list => InvoiceItems.ToList()
                    );
                // add the invoice
                _invoicesRepo.AddInvoice(myInvoice);
                // clean
                InvoiceItems.Clear();
                _selectedItem = null;
                Update();
            }
            else
            {
                Debug.WriteLine(_currentEmployee?.Id.ToString());
            }
        }

        private void Update()
        {
            _eventAggregator.GetEvent<CasherInvoiceItemsListChannel>().Publish(InvoiceItems);
            _eventAggregator.GetEvent<SelectedInvoiceItemShareChannel>().Publish(_selectedItem);
        }

    }
}
