using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureEditItem.Channels
{
    internal class EditSelectedItemShareChannel: PubSubEvent<Item?>
    {
    }
}
