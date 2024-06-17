using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Power_Hand.Models
{
    public class Emploee
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        public string? Password { get; set; }   

        // this is the things that the emploee has the othurity to do
        // TODO 
        public List<string>? Authorities { get; set; }

        // TODO
        public List<object>? Settings { get; set; }
    }
}
