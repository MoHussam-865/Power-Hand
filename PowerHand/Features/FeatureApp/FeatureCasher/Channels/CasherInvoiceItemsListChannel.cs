using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDatabase.Models;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureCasher.Channels
{
    public class CasherInvoiceItemsListChannel : PubSubEvent<ObservableCollection<InvoiceItem>>
    {
    }
}
