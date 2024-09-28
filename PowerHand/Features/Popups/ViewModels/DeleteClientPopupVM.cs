using MyDatabase.Models;
using MyDatabase.Repository.Clients;
using Power_Hand.Data.SharedData;
using Power_Hand.Features.FeatureApp.FeatureEditClient.Channels;
using Power_Hand.Utils.ViewModels;

namespace Power_Hand.Features.Popups.ViewModels
{
    public class DeleteClientPopupVM(IClientRepo clientRepo, IEventAggregator eventAggregator, SharedValuesStore store)
        : DeletePopupLogicVM<Client>(eventAggregator)
    {

        private readonly IClientRepo _clientRepo = clientRepo;
        private readonly IEventAggregator _eventAggregator = eventAggregator;

        public override string Title => "Action Can't be Undo";

        public override string Message => "You Are about to Delete one Client";

        public override Client? ThingToDelete { get => store.ClientToDelete; }

        public override void OnCancel() { }

        public override void OnDelete(Client client)
        {
            _clientRepo.DeleteClient(client);
            _eventAggregator.GetEvent<EditClientPageUpdateDatabaseChannel>().Publish();
        }
    }
}
