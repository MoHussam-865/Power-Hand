using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureEditItem.Channels
{
    public class EditItemCurrentFolderShareChannel: PubSubEvent<Item?>
    {
    }
}
