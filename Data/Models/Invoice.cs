using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public double Total { get; set; }
        public double? Discount { get; set; }
        public double VAT {  get; set; }
        public double? Payed { get; set; }
        public double? Remaining { get; set; }
        public string? Note {  get; set; }
        public string? InvoiceNote { get; set; }

        [NotMapped]
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();

    }
}
