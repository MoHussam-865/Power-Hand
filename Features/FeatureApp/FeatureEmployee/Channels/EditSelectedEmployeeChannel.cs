using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureEmployee.Channels
{
    public class EditSelectedEmployeeChannel: PubSubEvent<Employee?>
    {
    }
}
