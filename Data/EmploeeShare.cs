using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.Data
{
    public class EmploeeShare : PubSubEvent<Emploee>
    {
    }
}
