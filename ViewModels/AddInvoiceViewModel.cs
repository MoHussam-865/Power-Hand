using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.DBContext;
using Power_Hand.Interfaces;
using Power_Hand.Models;

namespace Power_Hand.ViewModels
{
    public class AddInvoiceViewModel : ViewModelBase
    {
        private readonly DatabaseContext _databaseContext;
        private Client? _client;
        private Emploee? _emploee;
        // notes for the staff
        private string? _invoiceNotes;
        // notes on the invoice
        private string? _notes;
        private ObservableCollection<InvoiceItem>? _chosenItems;
        private ObservableCollection<InvoiceItem>? _invoiceItems;

        // constructor
        public AddInvoiceViewModel(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            // load the items to choose from 

            // display the invoice items in case of invoice editing

        }

        // properties
        public Emploee? MyEmploee
        { get => _emploee; set { _emploee = value; OnPropertyChanged(); } }

        public Client? MyClient
        { get => _client; set { _client = value; OnPropertyChanged(); } }

        public ObservableCollection<InvoiceItem> InvoiceItems
        { get => _invoiceItems ?? []; set { _invoiceItems = value; OnPropertyChanged();}}
        
        public ObservableCollection<InvoiceItem> ChosenItems
        { get => _chosenItems = []; set { _chosenItems = value; OnPropertyChanged(); }}

        public string? MyInvoiceNotes
        { get => _invoiceNotes ?? ""; set { _invoiceNotes = value; OnPropertyChanged(); }}

        public string? Notes
        { get => _notes ?? ""; set { _notes = value; OnPropertyChanged(); }}


        // Methods
        private void GetItems(string parent) { }
        private void AddItem(InvoiceItem item) { }
        private void RemoveItem(InvoiceItem item) { }

        public void AddClient(Client client) { }
        public void RemoveClient(Client client) { }

        public void AddEmploee(Emploee emploee) { }

        public void AddNotes(string  notes) { }
        public void RemoveNotes(string notes) { }
        public void AddInvoiceNote(string note) { }
        public void RemoveInvoiceNote(string note) { }

    }
}
