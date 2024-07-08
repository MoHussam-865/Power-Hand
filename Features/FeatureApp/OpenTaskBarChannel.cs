using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Interfaces;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp
{
    public class OpenTaskBarChannel: PubSubEvent<bool>
    {
    }
}
