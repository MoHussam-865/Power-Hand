using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Data.Repository.Invoices;
using Power_Hand.Data.SharedData;
using Power_Hand.Models;
using Power_Hand.Utils.ViewModels;
using Prism.Events;

namespace Power_Hand.Features.Popups.ViewModels
{
    class DeleteInvoicePopupVM(IInvoicesRepo invoicesRepo, IEventAggregator eventAggregator, SharedValuesStore store)
        : DeletePopupLogicVM<Invoice>(eventAggregator)
    {

        private readonly IInvoicesRepo _invoicesRepo = invoicesRepo;

        public override string Title => "Action Can't be Undo";

        public override string Message => "You Are about to Delete one Invoice";

        public override Invoice? ThingToDelete { get => store.InvoiceToDelete; }

        public override void OnCancel() { }

        public override void OnDelete(Invoice invoice)
        {
            _invoicesRepo.DeleteInvoice(invoice);
        }
    }
}