using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDatabase.Models
{
    public class Invoice
    {
        private object items;

        [Key]
        public int Id { get; set; }
        public long Date { get; set; }
        public long? DueDate { get; set; }
        public int Type { get; set; }
        public int? ClientId { get; set; }

        public int EmployeeId { get; set; }
        public double? Discount { get; set; }
        public double? VAT { get; set; }
        public double Payed { get; set; }
        public double Remaining { get; set; }
        public string? Note { get; set; }

        // this is a note to the staff does not get printed
        public string? InvoiceNote { get; set; }
        public bool IsDeleted { get; set; } = false;

        [NotMapped]
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();

        public double Total { get => Items.Sum(x => x.Total); }


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
