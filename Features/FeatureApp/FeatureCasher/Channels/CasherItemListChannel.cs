using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureCasher.Channels
{
    public class CasherItemListChannel : PubSubEvent<Item?>
    {
    }
}
