using System.ComponentModel.DataAnnotations;
using Power_Hand.Data.enums;
using Power_Hand.Models;

namespace Power_Hand.Data.Models
{
    public class Settings
    {
        [Key]
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public EmployeeSettings Name { get; set; }
        public int Value { get; set; }
        public Employee? Employee { get; set; }

    }
}
