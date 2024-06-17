using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Power_Hand.Models
{
    public class InvoiceItem
    {
        [ForeignKey("Id")]
        public int InvoiceId { get; set; }
        [ForeignKey("Id")]
        public int ItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double Total { get; set; }
        public string? Notes {  get; set; }
        public int ParentId { get; set; }
        public double? Discount { get; set; }
        public bool IsFolder {  get; set; }


        // constructor
        public InvoiceItem(
            int itemId, string name,
            int parentId, double? discount,
            bool isFolder,
            double price,int invoiceId = 0,
            double quantity = 0, double total = 0,
            string? notes = null)
        {
            InvoiceId = invoiceId;
            ItemId = itemId;
            Name = name;
            Price = price;
            Quantity = quantity;
            Total = total;
            Notes = notes;
            ParentId = parentId;
            Discount = discount;
            IsFolder = isFolder;
        }

        // convert to Item
        public Item ToItem()
        {
            return new Item(id:ItemId, name:Name, price:Price, parent:ParentId,discount:Discount);
        }

    }
}
