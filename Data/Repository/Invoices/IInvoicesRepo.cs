using System.Collections.ObjectModel;
using Power_Hand.Models;

namespace Power_Hand.Data.Repository.Invoices
{
    public interface IInvoicesRepo
    {
        public void AddInvoice(Invoice invoice);

        public void EditInvoice(Invoice invoice);

        public void DeleteInvoice(Invoice invoice);

        // open spicific invoice
        public Task<Invoice> GetInvoiceById(int id);

        // get all the invoices between the date range 
        // if not set gets the un-setteled invoices (this shift)
        public Task<List<Invoice>> GetInvoices(long? startDate = null, long? endDate = null);


    }
}
