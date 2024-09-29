using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyDatabase.Models
{
    public class Invoice
    {
        private object items;

        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("date")]
        public long Date { get; set; }

        [NotMapped]
        [JsonPropertyName("items")]
        public List<InvoiceItem> Items { get; set; } = [];

        [JsonPropertyName("total")]
        public double Total { get => Items.Sum(x => x.Total); }

        [JsonPropertyName("note")]
        public string? Note { get; set; }

        [NotMapped]
        [JsonPropertyName("client")]
        public Client? ClientData { get; set; }


        [JsonIgnore]
        public long? DueDate { get; set; }

        [JsonIgnore]
        public int Type { get; set; }

        [JsonIgnore]
        public int? ClientId { get; set; }

        [JsonIgnore]
        public int EmployeeId { get; set; }

        [JsonIgnore]
        public double? Discount { get; set; }

        [JsonIgnore]
        public double? VAT { get; set; }

        [JsonIgnore]
        public double Payed { get; set; }

        [JsonIgnore]
        public double Remaining { get; set; }

        // this is a note to the staff does not get printed
        [JsonIgnore]
        public string? InvoiceNote { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;


        public Invoice() { }


        public Invoice(
            long date, int type, int employeeId,
            double payed, List<InvoiceItem> items, int id = 0, long? dueDate = null,
            int? clientId = null, double? discount = null, double? vat = null,
            double remaining = 0, string? note = null, string? invoiceNotes = null)
        {
            Id = id;
            Date = date;
            DueDate = dueDate;
            Type = type;
            ClientId = clientId;
            EmployeeId = employeeId;
            Discount = discount;
            VAT = vat;
            Payed = payed;
            Remaining = remaining;
            Note = note;
            InvoiceNote = invoiceNotes;
            Items = items;
        }

        public Invoice(long date, int type, int employeeId, double payed, int remaining, object items)
        {
            Date = date;
            Type = type;
            EmployeeId = employeeId;
            Payed = payed;
            Remaining = remaining;
            this.items = items;
        }
    }
}
