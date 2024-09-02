using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Data.enums;
using Power_Hand.Data.Models;

namespace Power_Hand.Models
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
