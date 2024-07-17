using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Data.Repository.Other;
using Power_Hand.Data.SharedData;
using Power_Hand.Models;
using Power_Hand.Utils.ViewModels;
using Prism.Events;

namespace Power_Hand.Features.Popups.ViewModels
{
    public class DeleteClientPopupVM(IClientRepo clientRepo, IEventAggregator eventAggregator, SharedValuesStore store)
        : DeletePopupLogicVM<Client>(eventAggregator)
    {

        private readonly IClientRepo _clientRepo = clientRepo;

        public override string Title => "Action Can't be Undo";

        public override string Message => "You Are about to Delete one Client";

        public override Client? ThingToDelete { get => store.ClientToDelete; }

        public override void OnCancel() { }

        public override void OnDelete(Client client)
        {
            _clientRepo.DeleteClient(client);
        }
    }
}
