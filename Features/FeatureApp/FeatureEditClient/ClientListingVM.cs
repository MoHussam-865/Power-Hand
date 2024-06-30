using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Other;
using Power_Hand.Features.FeatureApp.FeatureEditClient.Channels;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureEditClient
{
    public class ClientListingVM : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IClientRepo _clientsRepo;

        private Client? _selectedClient;
        public Client? SelectedClient
        {
            get => _selectedClient;
            set { _selectedClient = value; OnPropertyChanged(); }
        }

        private string? _search;

        public string? Search
        {
            get => _search;
            set
            {
                _search = value;
                GetClients();
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Client> _clients;

        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set { _clients = value; OnPropertyChanged(); }
        }




        public ICommand SelectClientCommand { get; set; }

        public ClientListingVM(
            IEventAggregator eventAggregator,
            IClientRepo clientsRepo)
        {
            _eventAggregator = eventAggregator;
            _clientsRepo = clientsRepo;
            _clients = [];
            GetClients();

            SelectClientCommand = new ClickCommand<Client>((c) => OnClientSelected(c));
            _eventAggregator.GetEvent<EditClientPageUpdateDatabaseChannel>().Subscribe(OnDatabaseUpdated);
        }

        private void OnDatabaseUpdated()
        {
            GetClients();
        }

        private void OnClientSelected(Client client)
        {
            SelectedClient = client;
            _eventAggregator.GetEvent<EditClientPageShareClientChannel>().Publish(client);
        }

        private async void GetClients()
        {
            if (_search == null)
            {
                Clients = [];
                return;
            }
            List<Client>? clients = await _clientsRepo.SearchClients(_search);
            Clients = clients != null ? new ObservableCollection<Client>(clients) : [];
        }
    }
}
