using System.Windows.Input;
using System.Windows.Navigation;
using Power_Hand.Data.SharedData;
using Power_Hand.Features.FeatureApp.FeatureEditClient.Channels;
using Power_Hand.Features.Popups.ViewModels;
using Power_Hand.Features.Popups;
using Prism.Events;
using MyDatabase.Models;
using MyDatabase.Repository.Clients;
using Power_Hand.Other.Other;

namespace Power_Hand.Features.FeatureApp.FeatureEditClient
{
    public class ClientFormVM : ViewModel
    {
        private Client? _currentClient;
        private readonly IEventAggregator _eventAggregator;
        private readonly IClientRepo _clientRepo;
        private readonly SharedValuesStore _valuesStore;
        private readonly INavigationService _navigationService;

        #region Client Properties

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

        #endregion

        public ICommand OnSaveCommand { get; set; }
        public ICommand OnDeleteCommand { get; set; }
        public ICommand OnDiscardCommand { get; set; }

        public ClientFormVM(
            IEventAggregator eventAggregator,
            SharedValuesStore sharedValuesStore,
            INavigationService navigationService,
            IClientRepo clientRepo)
        {
            _clientRepo = clientRepo;
            OnSaveCommand = new FunCommand(OnSaveClicked);
            OnDeleteCommand = new FunCommand(OnDeleteClicked);
            OnDiscardCommand = new FunCommand(OnCancelClicked);
            _eventAggregator = eventAggregator;
            _valuesStore = sharedValuesStore;
            _navigationService = navigationService;
            _eventAggregator.GetEvent<EditClientPageShareClientChannel>().Subscribe(OnClientSelected);
            FillIfCan();
        }

        private void OnClientSelected(Client? client)
        {
            _currentClient = client;
            FillIfCan();
        }

        private void OnCancelClicked() => Clear();
        

        private void OnDeleteClicked()
        {
            if (_currentClient != null)
            {
                _valuesStore.ClientToDelete = _currentClient;
                _navigationService.OpenPopup<DeleteClientPopupVM>();
                _eventAggregator.GetEvent<PopupCloseChannel>().Publish(false);
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

            if (!(string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(PhoneNumber) && string.IsNullOrEmpty(Address)))
            {
                int clientId = _currentClient == null ? 0 : _currentClient.Id;
                Client client = new()
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
                Clear();
            }
            
        }



        // fill the form if there is a selected client (edit or delete)
        private void FillIfCan()
        {
            Name = _currentClient?.Name;
            Address = _currentClient?.Address;
            Email = _currentClient?.Email;
            PhoneNumber = _currentClient?.PhoneNumber;
            Discount = _currentClient?.Discount.ToString();
            Notes = _currentClient?.Notes;
        }

        // clear all entries for new process
        private void Clear()
        {
            Name = null;
            Address = null;
            Email = null;
            PhoneNumber = null;
            Discount = null;
            Notes = null;
            _currentClient = null;
            _eventAggregator.GetEvent<EditClientPageUpdateDatabaseChannel>().Publish();
            _eventAggregator.GetEvent<EditClientPageShareClientChannel>().Publish(_currentClient);
        }
    }
}
