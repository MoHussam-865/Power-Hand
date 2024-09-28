using System.Collections.ObjectModel;
using MyDatabase.Models;

namespace MyDatabase.Repository.Invoices
{
    public interface IInvoicesRepo
    {
        public void AddInvoice(Invoice invoice);

        public Task<int> EditInvoice(Invoice invoice);

        public Task<int> DeleteInvoice(Invoice invoice);

        // open spicific invoice
        public Task<Invoice> GetInvoiceById(int id);

        // get all the invoices between the date range 
        // if not set gets the un-setteled invoices (this shift)
        public Task<List<Invoice>> GetInvoices(long? startDate = null, long? endDate = null);


    }
}
