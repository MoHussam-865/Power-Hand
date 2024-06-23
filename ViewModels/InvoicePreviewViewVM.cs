using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Data.Repository.Invoices;
using Power_Hand.Data.SharedData;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.ViewModels
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
