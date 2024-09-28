using System.ComponentModel.DataAnnotations;
using MyDatabase.enums;

namespace MyDatabase.Models
{
    public class Employee(string name, string password)
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; } = name;
        public string? Password { get; set; } = password;

        // this is the things that the employee has the authority to do
        public EmployeeRules Rule { get; set; } = EmployeeRules.Casher;

        public List<Settings>? Settings { get; set; }
    }
}
