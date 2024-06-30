using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Power_Hand.Data.Models;
using Power_Hand.Models;


namespace Power_Hand.Data.Repository.Invoices
{
    public class InvoicesRepoImpl(DatabaseContext database) : IInvoicesRepo
    {
        private readonly DatabaseContext _database = database;

        public async void AddInvoice(Invoice invoice)
        {
            _database.Invoice.Add(invoice);
            await _database.SaveChangesAsync();



            invoice.Items.ForEach(item =>
            {
                item.InvoiceId = invoice.Id;

                _database.InvoiceItem.Add(item);

            });
            await _database.SaveChangesAsync();

        }


        public async void DeleteInvoice(Invoice invoice)
        {
            foreach (InvoiceItem item in invoice.Items)
            {
                _database.InvoiceItem.Remove(item);
            }
            _database.Invoice.Remove(invoice);
            await _database.SaveChangesAsync();
        }


        public async void EditInvoice(Invoice invoice)
        {
            foreach (InvoiceItem item in invoice.Items)
            {
                _database.InvoiceItem.Update(item);
            }
            _database.Invoice.Update(invoice);
            await _database.SaveChangesAsync();
        }


        public async Task<Invoice> GetInvoiceById(int id)
        {
            return await _database.Invoice.Include(invoice => invoice.Items)
                .FirstAsync(invoice => invoice.Id == id);
        }

        public async Task<List<Invoice>> GetInvoices(long? startDate = null, long? endDate = null)
        {
            return await _database.Invoice.Include(invoice => invoice.Items).ToListAsync();
        }
    }
}
