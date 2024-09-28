using System.Collections.ObjectModel;
using System.Windows.Input;
using MyDatabase.Models;
using MyDatabase.Repository.Invoices;
using Power_Hand.Other.Other;
using Power_Hand.Other.SharedData;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureInvoicesPreview
{
    public class InvoicesListViewVM : ViewModel
    {
        private IInvoicesRepo _invoicesRepo;
        private IEventAggregator _eventAggregator;

        private Invoice? _selectedInvoice;
        public Invoice? SelectedInvoice
        {
            get => _selectedInvoice;
            set { _selectedInvoice = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Invoice> _invoices;
        public ObservableCollection<Invoice> Invoices
        {
            get => _invoices;
            set { _invoices = value; OnPropertyChanged(); }
        }



        public ICommand ViewInvoiceCommand { get; set; }

        public InvoicesListViewVM(
            IInvoicesRepo invoicesRepo,
            IEventAggregator eventAggregator)
        {
            _invoicesRepo = invoicesRepo;
            _eventAggregator = eventAggregator;

            _invoices = [];
            GetInvoices();

            ViewInvoiceCommand = new ClickCommand<Invoice>((x) => OnInvoiceSelected(x));
        }

        private async void GetInvoices()
        {
            List<Invoice> invoices = await _invoicesRepo.GetInvoices();
            Invoices = new ObservableCollection<Invoice>(invoices);
        }

        private void OnInvoiceSelected(Invoice invoice)
        {
            SelectedInvoice = invoice;
            _eventAggregator.GetEvent<SelectedInvoiceShare>().Publish(invoice);
        }

    }
}
