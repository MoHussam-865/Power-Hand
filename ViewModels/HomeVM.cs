using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Power_Hand.Commands;
using Power_Hand.Interfaces;

namespace Power_Hand.ViewModels
{
    class HomeVM : ViewModel
    {
        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
            set
            {
                _navigationService = value;
                OnPropertyChanged(nameof(_navigationService));
            }
        }

        public ICommand OnLoginCommand { get; set; }

        public HomeVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            OnLoginCommand = new RelayCommand<string>((x) => OnLoginClicked(x));
            
        }


        private void OnLoginClicked(string password)
        {
            // hash the input text
            // check if there is a user with the same hash
            if (!string.IsNullOrEmpty(password) && password=="123")
            {
                NavigationService.NavigateTo<CasherVM>();
            }
        }


    }
}
