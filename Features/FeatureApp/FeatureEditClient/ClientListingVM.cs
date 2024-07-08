using System.Collections.ObjectModel;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Other;
using Power_Hand.Features.FeatureApp.FeatureEditClient.Channels;
using Power_Hand.Models;
using Power_Hand.Utils.ViewModels;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureEditClient
{
    /// <summary>
    /// implements SearchLogicVM takes a generic type and has two properties Search String & Search results
    /// 
    /// The abstract method OnSearchChanged Updates the Search results whenever the search changes
    /// The GetItems(); is a public method that call OnSearchChanged To Refresh (Update the View) on Database Changes
    /// </summary>
    public class ClientListingVM : SearchLogicVM<Client>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IClientRepo _clientsRepo;

        private Client? _selectedClient;
        public Client? SelectedClient
        {
            get => _selectedClient;
            set { _selectedClient = value; OnPropertyChanged(); }
        }

      

        public ICommand SelectClientCommand { get; set; }

        public ClientListingVM(
            IEventAggregator eventAggregator,
            IClientRepo clientsRepo)
        {
            _eventAggregator = eventAggregator;
            _clientsRepo = clientsRepo;
            
            SelectClientCommand = new ClickCommand<Client>((c) => OnClientSelected(c));
            _eventAggregator.GetEvent<EditClientPageUpdateDatabaseChannel>().Subscribe(OnDatabaseUpdated);
        }

        private void OnDatabaseUpdated() => GetItems();
       

        private void OnClientSelected(Client client)
        {
            SelectedClient = client;
            _eventAggregator.GetEvent<EditClientPageShareClientChannel>().Publish(client);
        }

        public async override Task<List<Client>> OnSearchChanged(string? search)
        {
            if (search == null) return [];
            
            List<Client>? clients = await _clientsRepo.SearchClients(search);
            return clients ?? [];
        }
    }
}
