using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Other;
using Power_Hand.Data.SharedData;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.ViewModels
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
                GetClients(value);
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Client> _clients;

        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set { _clients = value; OnPropertyChanged(); }
        }




        ICommand SelectClientCommand { get; set; }

        public ClientListingVM(
            IEventAggregator eventAggregator,
            IClientRepo clientsRepo)
        {
            _eventAggregator = eventAggregator;
            _clientsRepo = clientsRepo;
            _clients = [];
            GetClients(_search);

            SelectClientCommand = new ClickCommand<Client>((c) => OnClientSelected(c));
        }

        private void OnClientSelected(Client client)
        {
            SelectedClient = client;
            _eventAggregator.GetEvent<ClientShare>().Publish(client);
        }

        private async void GetClients(string? search)
        {
            if (search == null)
            {
                Clients = [];
                return;
            }
            List<Client>? clients = await _clientsRepo.SearchClients(search);
            Clients = (clients != null) ? new ObservableCollection<Client>(clients) : [];
        }
    }
}
