

using MyDatabase.Models;

namespace Power_Hand.Data.SharedData
{
    public class SelectedInvoiceItemToDeleteChannel : PubSubEvent<InvoiceItem>
    {
    }
}
