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
using Power_Hand.Utils.ViewModels;

namespace Power_Hand.Features.FeatureApp.FeatureCasher
{
    public class InvoiceItemsListVM : ViewModel
    {

        // used to pass data between view models
        private readonly IEventAggregator _eventAggregator;
        private readonly CalculatorVM _calculator;

        private ObservableCollection<InvoiceItem> _invoiceItems;
        public ObservableCollection<InvoiceItem> InvoiceItems
        {
            get => _invoiceItems;
            set { _invoiceItems = value; OnPropertyChanged(); }
        }

        public ICommand ItemSelectCommand { get; set; }

        // constructor with dependency injection
        public InvoiceItemsListVM(IEventAggregator eventAggregator,CalculatorVM calculator)
        {
            // no invoice items yet
            _invoiceItems = [];
            _calculator = calculator;

            // initiate commands here
            ItemSelectCommand = new ClickCommand<InvoiceItem>((x) => OnItemSelected(x));


            // gets the current employee passed from the HomeVM 
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<CasherItemListChannel>().Subscribe(OnItemChosen);
            _eventAggregator.GetEvent<CasherInvoiceItemsListChannel>().Subscribe(OnInvoiceItemsChanged);
        }

        
        /// <summary>
        /// whenever item Chosen to be added to the invoice this method listen for CasherItemListChannel
        /// and add the item to the invoice
        /// </summary>
        /// <param name="item"></param>
        private void OnItemChosen(Item? item)
        {
            if (item != null)
            {
                var x = _calculator.GetValue();
                InvoiceItems.Add(item.ToInvoiceItem(x));
                _eventAggregator.GetEvent<CasherInvoiceItemsListChannel>().Publish(InvoiceItems);
            }
        }

        private void OnInvoiceItemsChanged(ObservableCollection<InvoiceItem> list)
        {
            InvoiceItems = new(list);
        }

        // the selected item is then used for edit or delete one item
        private void OnItemSelected(InvoiceItem item)
        {
            _eventAggregator?.GetEvent<SelectedInvoiceItemShareChannel>().Publish(item);
        }

    }
}
