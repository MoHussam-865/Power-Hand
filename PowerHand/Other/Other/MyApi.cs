using MyDatabase.Models;
using Refit;

namespace Power_Hand.Interfaces
{
    public interface MyApi
    {
        [Get("/data")]
        Task<IEnumerable<Invoice>> GetInvoice(int id);

        [Post("/data")]
        Task<Invoice> SendInvoice(Invoice invoice);
    }
}
