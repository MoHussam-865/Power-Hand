using Power_Hand.Interfaces;

namespace Power_Hand.Features.FeatureApp.FeatureEditClient
{
    public class AddEditClientPageVM : ViewModel
    {
        private ClientFormVM _clientFormVM;
        public ClientFormVM ClientFormVM
        {
            get => _clientFormVM;
            set { _clientFormVM = value; OnPropertyChanged(); }
        }

        private NavigationBarVM _navigationVM;
        public NavigationBarVM NavigationVM
        {
            get => _navigationVM;
            set { _navigationVM = value; OnPropertyChanged(); }
        }

        private ClientListingVM _clientListingVM;
        public ClientListingVM ClientListingVM
        {
            get => _clientListingVM;
            set { _clientListingVM = value; OnPropertyChanged(); }
        }




        public AddEditClientPageVM(
            ClientFormVM clientFormVM,
            ClientListingVM clientListingVM,
            NavigationBarVM navigationBarVM)
        {
            _clientFormVM = clientFormVM;
            _clientListingVM = clientListingVM;
            _navigationVM = navigationBarVM;
        }

    }
}
