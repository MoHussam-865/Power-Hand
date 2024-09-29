using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyDatabase.Models
{
    public class InvoiceItem
    {
        
        [Key]
        [JsonIgnore]
        public int Id { get; set; } = 0;

        //[ForeignKey("Id")]
        [JsonIgnore]
        public int InvoiceId { get; set; }
        //[ForeignKey("Id")]

        #region Invoice 
        // just for query purpose
        [JsonIgnore]
        public Invoice? Invoice { get; set; }
        //public Item? Item{ get; set; }

        #endregion

        [JsonPropertyName("id")]
        public int ItemId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("qty")]
        public double Quantity { get; set; }

        [JsonIgnore]
        public string? Notes { get; set; }


        public int ParentId { get; set; }

        [JsonPropertyName("discount")]
        public double Discount { get; set; } = 0;

        [JsonIgnore]
        public double Total { get => Price * Quantity * (1 - Discount); }

        [JsonIgnore]
        public bool IsFolder { get; set; }

        [JsonIgnore]
        public string? ImagePath { get; set; } = null;


        // constructor
        
        public InvoiceItem() { }


        public InvoiceItem(
            int itemId, string name,
            int parentId, bool isFolder,
            double price,int id = 0, double discount = 0, int invoiceId = 0,
            double quantity = 1, string? notes = null, string? imagePath = null)
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
            ImagePath = imagePath;
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
