using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Power_Hand.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public long Date { get; set; }
        public long? DueDate { get; set; }
        public int Type { get; set; }
        public int? ClientId { get; set; }

        public int EmploeeId { get; set; }
        public double? Discount { get; set; }
        public double? VAT { get; set; }
        public double Payed { get; set; }
        public double Remaining { get; set; }
        public string? Note { get; set; }

        // this is a note to the staff does not get printed
        public string? InvoiceNote { get; set; }

        [NotMapped]
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();

        public double Total { get => Items.Sum(x => x.Total); }

        public Invoice(
            long date, int type, int emploeeId,
            double payed, List<InvoiceItem> items, int id = 0, long? dueDate = null,
            int? clientId = null, double? discount = null, double? vat = null,
            double remaining = 0, string? note = null, string? invoiceNotes = null)
        {
            Id = id;
            Date = date;
            DueDate = dueDate;
            Type = type;
            ClientId = clientId;
            EmploeeId = emploeeId;
            Discount = discount;
            VAT = vat;
            Payed = payed;
            Remaining = remaining;
            Note = note;
            InvoiceNote = invoiceNotes;
            Items = items;
        }

    }
}
