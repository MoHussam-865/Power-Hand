using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Power_Hand.Models
{
    public class InvoiceItem
    {
        
        [Key]
        public int Id { get; set; } = 0;

        //[ForeignKey("Id")]
        public int InvoiceId { get; set; }
        //[ForeignKey("Id")]

        #region Invoice 
        // just for query purpose
        public Invoice? Invoice { get; set; }
        //public Item? Item{ get; set; }

        #endregion

        public int ItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Quantity { get; set; }
        public string? Notes { get; set; }
        public int ParentId { get; set; }
        public double Discount { get; set; } = 0;
        public double Total { get => Price * Quantity * (1 - Discount); }
        public bool IsFolder { get; set; }


        // constructor
        
        public InvoiceItem() { }


        public InvoiceItem(
            int itemId, string name,
            int parentId, bool isFolder,
            double price,int id = 0, double discount = 0, int invoiceId = 0,
            double quantity = 1, string? notes = null)
        {
            Id = id;
            InvoiceId = invoiceId;
            ItemId = itemId;
            Name = name;
            Price = price;
            Quantity = quantity;
            Notes = notes;
            ParentId = parentId;
            Discount = discount;
            IsFolder = isFolder;
        }

        // convert to Item
        public Item ToItem()
        {
            return new Item(id: ItemId, name: Name, price: Price, parent: ParentId, discount: Discount);
        }


        public bool Equals(InvoiceItem invoiceItem)
        {
            return Name == invoiceItem.Name && Price == invoiceItem.Price &&
                ParentId == invoiceItem.ParentId && Notes == invoiceItem.Notes &&
                ItemId == invoiceItem.ItemId && Discount == invoiceItem.Discount;
        }


    }
}
