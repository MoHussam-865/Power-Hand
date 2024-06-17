using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Power_Hand.Interfaces;

namespace Power_Hand.ViewModels
{
    class MainVM : ViewModel
    {
        private INavigationService _navigationService;
        public INavigationService MyNavigationService
        {
            get => _navigationService;
            set
            {
                _navigationService = value;
                OnPropertyChanged(nameof(_navigationService));
            }
        }

        public MainVM(INavigationService navigationService)
        {
            MyNavigationService = navigationService;
            MyNavigationService.NavigateTo<HomeVM>();
        }

    }
}
