using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Invoices;
using Power_Hand.Data.Repository.Items;
using System.Windows.Input;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;
using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;

namespace Power_Hand.Features.FeatureApp.FeatureCasher
{
    public class InvoiceItemsListVM : ViewModel
    {

        // used to pass data between view models
        private readonly IEventAggregator _eventAggregator;

        private ObservableCollection<InvoiceItem> _invoiceItems;
        public ObservableCollection<InvoiceItem> InvoiceItems
        {
            get => _invoiceItems;
            set { _invoiceItems = value; OnPropertyChanged(); }
        }


        public ICommand ItemSelectCommand { get; set; }



        // constructor with dependancy injection
        public InvoiceItemsListVM(IEventAggregator eventAggregator)
        {
            // no invoice items yet
            _invoiceItems = [];

            // initiate commands here
            ItemSelectCommand = new ClickCommand<InvoiceItem>((x) => OnItemSelected(x));

            // gets the current emploee passed from the HomeVM 
            _eventAggregator = eventAggregator;


            _eventAggregator.GetEvent<CasherItemListChannel>().Subscribe(OnItemSelected);
            _eventAggregator.GetEvent<CasherInvoiceItemsListChannel>().Subscribe(OnInvoiceItemsChanged);
        }

        private void OnItemSelected(Item? item)
        {
            if (item != null)
            {
                InvoiceItems.Add(item.ToInvoiceItem());
                _eventAggregator.GetEvent<CasherInvoiceItemsListChannel>().Publish(InvoiceItems);
            }
        }
        private void OnInvoiceItemsChanged(ObservableCollection<InvoiceItem> list)
        {
            InvoiceItems = list;
        }

        // the selected item is then used for edit or delete one item
        private void OnItemSelected(InvoiceItem item)
        {
            _eventAggregator?.GetEvent<SelectedInvoiceItemShareChannel>().Publish(item);
        }

    }
}
