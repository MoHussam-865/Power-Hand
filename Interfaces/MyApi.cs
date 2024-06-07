using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Models;
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
