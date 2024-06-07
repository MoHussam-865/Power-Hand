using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Power_Hand.Models
{
    // this items is stored in the database but never displayed directly
    // (till now) its transformed to InvoiceItem.cs before displaed
    public class Item
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Parent { get; set; } = string.Empty;
    }
}
