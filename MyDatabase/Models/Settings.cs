using System.ComponentModel.DataAnnotations;
using MyDatabase.enums;

namespace MyDatabase.Models
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
