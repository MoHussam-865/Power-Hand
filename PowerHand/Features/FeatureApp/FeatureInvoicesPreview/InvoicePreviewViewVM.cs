using MyDatabase.Models;
using Power_Hand.Other.Other;
using Power_Hand.Other.SharedData;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureInvoicesPreview
{
    public class InvoicePreviewViewVM : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private Invoice? _selectedInvoice;
        public Invoice? SelectedInvoice
        {
            get => _selectedInvoice;
            set
            {
                _selectedInvoice = value;
                OnPropertyChanged();
                DisplayInvoice();
            }

        }

        public InvoicePreviewViewVM(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<SelectedInvoiceShare>().Subscribe(OnInvoiceSelected);
        }


        private void OnInvoiceSelected(Invoice? invoice) => SelectedInvoice = invoice;

        private void DisplayInvoice()
        {
            if (_selectedInvoice != null)
            {
                // Do something
            }
        }
    }
}
