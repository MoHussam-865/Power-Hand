using MyDatabase.Models;
using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;
using Power_Hand.Other.Other;
using Power_Hand.Other.SharedData;

namespace Power_Hand.Data.SharedData
{
    public class SharedValuesStore : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        private Item? _item;
        public Item? SharedItem { get => _item; set { _item = value; OnPropertyChanged(); } }

        private Item? _folder;
        public Item? SharedFolder { get => _folder; set { _folder = value; OnPropertyChanged(); } }

        private Employee? _employee;
        public Employee? Employee { get => _employee; set { _employee = value; OnPropertyChanged(); } }

        private InvoiceItem? _invoiceItem;
        public InvoiceItem? InvoiceItem { get => _invoiceItem; set { _invoiceItem = value; OnPropertyChanged(); } }
        
        public double CalculatorValue { get; set; }

        


        public SharedValuesStore(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<CasherItemListChannel>().Subscribe(OnItemSelected);
            _eventAggregator.GetEvent<CasherFolderShareChannel>().Subscribe(OnFolderOpened);
            _eventAggregator.GetEvent<EmploeeShare>().Subscribe(OnEmployeeLogin);
            _eventAggregator.GetEvent<SelectedInvoiceItemShareChannel>().Subscribe(OnInvoiceItemSelected);

            _eventAggregator.GetEvent<SelectedClientToDeleteChannel>().Subscribe(OnClientSelectedToDelete);
            _eventAggregator.GetEvent<SelectedEmployeeToDeleteChannel>().Subscribe(OnEmployeeSelectedToDelete);
            _eventAggregator.GetEvent<SelectedInvoiceItemToDeleteChannel>().Subscribe(OnInvoiceItemSelectedToDelete);
            _eventAggregator.GetEvent<SelectedInvoiceToDeleteChannel>().Subscribe(OnInvoiceSelectedToDelete);
            _eventAggregator.GetEvent<SelectedItemToDeleteChannel>().Subscribe(OnItemSelectedToDelete);
        }



        #region things to delete
        public InvoiceItem? InvoiceItemToDelete { get; set; }
        public Item? ItemToDelete { get; set; }
        public Employee? EmployeeToDelete { get; set; }
        public Client? ClientToDelete { get; set; }
        public Invoice? InvoiceToDelete { get; set; }

        private void OnItemSelectedToDelete(Item item) => ItemToDelete = item;
        private void OnInvoiceSelectedToDelete(Invoice invoice) => InvoiceToDelete = invoice;
        private void OnInvoiceItemSelectedToDelete(InvoiceItem item) => InvoiceItemToDelete = item;
        private void OnEmployeeSelectedToDelete(Employee employee) => EmployeeToDelete = employee;
        private void OnClientSelectedToDelete(Client client) => ClientToDelete = client;
        #endregion


        private void OnInvoiceItemSelected(InvoiceItem? item) => InvoiceItem = item;
        private void OnEmployeeLogin(Employee employee) => Employee = employee;
        private void OnFolderOpened(Item? item) => SharedFolder = item;
        private void OnItemSelected(Item? item) => SharedItem = item;
        
    }
}
