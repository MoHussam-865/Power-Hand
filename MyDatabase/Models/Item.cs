using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyDatabase.Models
{
    public class Item
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("parentId")]
        public int ParentId { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;

        [JsonPropertyName("isFolder")]
        public bool IsFolder { get; set; }

        [JsonIgnore]
        public double? Expence { get; set; }

        [JsonPropertyName("notes")]
        public string? Note { get; set; }

        [JsonPropertyName("discount")]
        public double Discount { get; set; } = 0;

        [JsonPropertyName("lastUpdate")]
        public int LastUpdate { get; set; } = 0;

        [JsonPropertyName("imageAbsolutePath")]
        public string? ImageAbsolutePath { get; set; } = null;

        [JsonPropertyName("imageUrl")]
        public string? ImageUrl { get; set; } = null;


        [NotMapped]
        [JsonIgnore]
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
            ImageAbsolutePath = imagePath;
        }

        public Item() { }

        // convert to InvoiceItem
        public InvoiceItem ToInvoiceItem(double qty = 1)
        {
            // set The Quantity directly
            return new InvoiceItem(itemId: Id, name: Name,
            price: Price, parentId: ParentId, discount: Discount,
            quantity: qty, isFolder: IsFolder, imagePath: ImageAbsolutePath);
            // notes, total & InvoiceId to be set later
        }
    }
}
