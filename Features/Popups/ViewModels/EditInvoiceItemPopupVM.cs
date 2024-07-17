using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.SharedData;
using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.Features.Popups.ViewModels
{
    public class EditInvoiceItemPopupVM: ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly SharedValuesStore _store;

        private InvoiceItem? _invoiceItem;
        public InvoiceItem? MyInvoiceItem{
            get => _invoiceItem;
            set {  
                _invoiceItem = value;  
                OnPropertyChanged();
                ItemName = value?.Name;
                ItemQty = value?.Quantity.ToString();
            }
        }

        private string? _itemName;
        public string? ItemName
        {
            get => _itemName;
            set { _itemName = value; OnPropertyChanged(); }
        }

        private string? _itemQty;
        public string? ItemQty {
            get => _itemQty;
            set { _itemQty = value; OnPropertyChanged(); }
        }

        private string? _errorMsg;
        public string? ErrorMsg
        {
            get => _errorMsg;
            set { _errorMsg = value; OnPropertyChanged(); }
        }

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        public EditInvoiceItemPopupVM(
            SharedValuesStore store,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _store = store;
            _invoiceItem = store.InvoiceItem;
            _itemName = _invoiceItem?.Name;
            _itemQty = _invoiceItem?.Quantity.ToString();

            _eventAggregator.GetEvent<SelectedInvoiceItemShareChannel>().Subscribe(OnItemChanged);

            SubmitCommand = new FunCommand(OnQuantityChanges);
            CancelCommand = new FunCommand(OnCancelClicked);
        }

        private void OnCancelClicked()
        {
            CloseWindow();
            _eventAggregator.GetEvent<SelectedInvoiceItemShareChannel>().Publish(null);
        }

        private void OnItemChanged(InvoiceItem? item) => MyInvoiceItem = item;

        private void OnQuantityChanges()
        {
            if (ItemQty == null || MyInvoiceItem == null) return;
            
            try
            {
                double quantity = double.Parse(ItemQty);
                MyInvoiceItem.Quantity = quantity;
                _eventAggregator.GetEvent<ItemQuantityEditedChannel>().Publish(quantity);
                CloseWindow();
            } catch 
            {
                ErrorMsg = "Quantity Error";
            }
        }

        private void CloseWindow()
        {
            _itemName = null;
            _itemQty = null;
            _eventAggregator.GetEvent<PopupCloseChannel>().Publish(true);
        }
 
    }
}
