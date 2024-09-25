using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Power_Hand.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
        public int ParentId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsFolder { get; set; }

        public double? Expence { get; set; }
        public string? Note { get; set; }
        public double Discount { get; set; } = 0;
        public int LastUpdate { get; set; } = 0;
        public string? ImagePath { get; set; } = null;

        [NotMapped]
        public bool IsSelected { get; set; } = false;

        // constructor
        public Item(int id,
            string name,
            double price, int parent,
            string? description = null,
            double? expence = null, string? note = null,
            double discount = 0, bool isFolder = false, bool isDeleted = false,
            string? imagePath = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ParentId = parent;
            Expence = expence;
            Note = note;
            Discount = discount;
            IsDeleted = isDeleted;
            IsFolder = isFolder;
            ImagePath = imagePath;
        }

        public Item() { }

        // convert to InvoiceItem
        public InvoiceItem ToInvoiceItem(double qty = 1)
        {
            // set The Quantity directly
            return new InvoiceItem(itemId: Id, name: Name,
            price: Price, parentId: ParentId, discount: Discount,
            quantity: qty, isFolder: IsFolder, imagePath: ImagePath);
            // notes, total & InvoiceId to be set later
        }
    }
}
