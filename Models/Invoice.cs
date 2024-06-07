using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Power_Hand.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public long Date { get; set; }
        public int ClientId { get; set; }

        public int EmploeeId { get; set; }
        public List<InvoiceItem>? Items {get; set;}
    
    
    }
}
