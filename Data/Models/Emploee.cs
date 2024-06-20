using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Power_Hand.Models
{
    public class Emploee
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }

        // this is the things that the emploee has the othurity to do
        // TODO 
        [NotMapped]
        public List<string>? Authorities { get; set; }

        // TODO
        [NotMapped]
        public List<object>? Settings { get; set; }
    }
}
