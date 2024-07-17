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
            // empty list to add items to it 
            List<InvoiceItem> uniqueItems = [];
            List<InvoiceItem> items = invoice.Items;

            while (items.Count > 0)
            {
                // remove the first item
                InvoiceItem item = items.First();
                items.Remove(item);

                // search the items list for it 
                while (items.Count > 0)
                {
                    InvoiceItem currentItem = items.First();
                    // if the item id == the item id in the list we add the quantities
                    // and remove it from the list else we continue
                    if (currentItem.Id == item.Id)
                    {
                        item.Quantity += currentItem.Quantity;
                        items.Remove(currentItem);
                    }
                }
                // finaly add it to the uniqe list
                uniqueItems.Add(item);
            }
            invoice.Items = uniqueItems;

            // add the invoice to the database
            _database.Invoice.Add(invoice);
            await _database.SaveChangesAsync();
        }


        public async Task<int> DeleteInvoice(Invoice invoice)
        {
            // foreach (InvoiceItem item in invoice.Items) { _database.InvoiceItem.Remove(item); }
            // _database.Invoice.Remove(invoice);
            // await _database.SaveChangesAsync();
            invoice.IsDeleted = true;
            return await EditInvoice(invoice);
        }


        public async Task<int> EditInvoice(Invoice invoice)
        {
            // foreach (InvoiceItem item in invoice.Items) { _database.InvoiceItem.Update(item);}
            _database.Invoice.Update(invoice);
            return await _database.SaveChangesAsync();
        }


        public async Task<Invoice> GetInvoiceById(int id)
        {
            return await _database.Invoice.Where(invoice => !invoice.IsDeleted).Include(invoice => invoice.Items)
                .FirstAsync(invoice => invoice.Id == id);
        }

        public async Task<List<Invoice>> GetInvoices(long? startDate = null, long? endDate = null)
        {
            return await _database.Invoice.Where(invoice => !invoice.IsDeleted).Include(invoice => invoice.Items).ToListAsync();
        }
    }
}
