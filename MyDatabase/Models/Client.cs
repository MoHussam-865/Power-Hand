using System.ComponentModel.DataAnnotations;

namespace MyDatabase.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public double Discount { get; set; } = 0;
        public string? Notes { get; set; }
    }
}
