using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.Data.SharedData
{
    public class SharedValuesStore: ViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        private Item? _item;
        public Item? SharedItem { get =>_item; set { _item = value; OnPropertyChanged(); } }
        private Item? _folder;
        public Item? SharedFolder { get => _folder; set { _folder = value; OnPropertyChanged(); } }
        private Emploee? _emploee;
        public Emploee? Emploee { get => _emploee; set { _emploee = value; OnPropertyChanged(); } }
        private InvoiceItem? _invoiceItem;
        public InvoiceItem? InvoiceItem { get => _invoiceItem; set { _invoiceItem = value; OnPropertyChanged(); } }

        public SharedValuesStore(IEventAggregator eventAggregator) 
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ItemShare>().Subscribe(OnItemSelected);
            _eventAggregator.GetEvent<FolderShare>().Subscribe(OnFolderOpened);
            _eventAggregator.GetEvent<EmploeeShare>().Subscribe(OnEmploeeLogin);
            _eventAggregator.GetEvent<SelectedInvoiceItemShare>().Subscribe(OnInvoiceItemSelected);
        }

        private void OnInvoiceItemSelected(InvoiceItem? item) => InvoiceItem = item;

        private void OnEmploeeLogin(Emploee emploee) => Emploee = emploee;

        private void OnFolderOpened(Item? item) => SharedFolder = item;

        private void OnItemSelected(Item? item) => SharedItem = item;
    }
}
