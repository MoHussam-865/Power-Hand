using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Data.Repository.Invoices;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Data.SharedData;
using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;
using Power_Hand.Migrations;
using Power_Hand.Models;
using Power_Hand.Utils.ViewModels;
using Prism.Events;

namespace Power_Hand.Features.Popups.ViewModels
{
    public class DeleteInvoiceItemPopupVM(IEventAggregator eventAggregator, SharedValuesStore store)
        : DeletePopupLogicVM<InvoiceItem>(eventAggregator)
    {
        private readonly IEventAggregator _eventAggregator = eventAggregator;

        public override string Title => "Action Can't be Undo";

        public override string Message => "You Are about to Delete one Item";

        public override InvoiceItem? ThingToDelete { get => store.InvoiceItemToDelete; }

        public override void OnCancel()
        {
            _eventAggregator.GetEvent<RemoveInvoiceItemFromListChannel>().Publish(null);
            _eventAggregator.GetEvent<PopupCloseChannel>().Publish(true);

        }

        public override void OnDelete(InvoiceItem item)
        {
            // remove from the invoice
            _eventAggregator.GetEvent<RemoveInvoiceItemFromListChannel>().Publish(ThingToDelete);
            _eventAggregator.GetEvent<PopupCloseChannel>().Publish(true);
        }
    }
}

