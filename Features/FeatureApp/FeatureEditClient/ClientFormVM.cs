using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Other;
using Power_Hand.Data.SharedData;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureEditClient
{
    public class ClientFormVM : ViewModel
    {
        private Client? _currentClient;
        private readonly IEventAggregator _eventAggregator;
        private readonly IClientRepo _clientRepo;

        private string? _name;
        public string? Name { get => _name; set { _name = value; OnPropertyChanged(); } }

        private string? _address;
        public string? Address { get => _address; set { _address = value; OnPropertyChanged(); } }

        private string? _email;
        public string? Email { get => _email; set { _email = value; OnPropertyChanged(); } }

        private string? _phoneNumber;
        public string? PhoneNumber { get => _phoneNumber; set { _phoneNumber = value; OnPropertyChanged(); } }

        private string? _discount;
        public string? Discount { get => _discount; set { _discount = value; OnPropertyChanged(); } }

        private string? _notes;
        public string? Notes { get => _notes; set { _notes = value; OnPropertyChanged(); } }


        public ICommand OnSaveCommand { get; set; }
        public ICommand OnDeleteCommand { get; set; }
        public ICommand OnDiscardCommand { get; set; }

        public ClientFormVM(
            IEventAggregator eventAggregator,
            IClientRepo clientRepo)
        {
            _clientRepo = clientRepo;
            OnSaveCommand = new FunCommand(OnSaveClicked);
            OnDeleteCommand = new FunCommand(OnDeleteClicked);
            OnDiscardCommand = new FunCommand(OnCancelClicked);
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ClientShare>().Subscribe(OnClientSelected);
            FillIfCan();
        }

        private void OnCancelClicked()
        {
            Clear();
        }

        private async void OnDeleteClicked()
        {
            if (_currentClient != null)
            {
                await _clientRepo.DeleteClient(_currentClient);
                Clear();
            }
        }

        private async void OnSaveClicked()
        {

            double discount = 0;
            try
            {
                discount = Discount == null ? 0 : double.Parse(Discount);
            }
            catch { }

            int clientId = _currentClient == null ? 0 : _currentClient.Id;
            Client client = new Client()
            {
                Id = clientId,
                Name = Name,
                Address = Address,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Discount = discount,
                Notes = Notes,

            };

            // then we are adding a new client
            if (_currentClient == null)
            {
                await _clientRepo.AddClient(client);
            }
            else
            {
                await _clientRepo.UpdateClient(client);
            }
        }

        private void OnClientSelected(Client? client)
        {
            _currentClient = client;
            FillIfCan();
        }



        private void FillIfCan()
        {
            if (_currentClient != null)
            {
                _name = _currentClient.Name;
                _address = _currentClient.Address;
                _email = _currentClient.Email;
                _phoneNumber = _currentClient.PhoneNumber;
                _discount = _currentClient.Discount.ToString();
                _notes = _currentClient.Notes;
            }
        }

        private void Clear()
        {
            Name = null;
            Address = null;
            Email = null;
            PhoneNumber = null;
            Discount = null;
            Notes = null;
            _currentClient = null;
            _eventAggregator.GetEvent<ClientShare>().Publish(_currentClient);
        }
    }
}
