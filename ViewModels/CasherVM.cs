using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Interfaces;

namespace Power_Hand.ViewModels
{
    class CasherVM: ViewModel
    {
        private INavigationService _navigationService;
        public INavigationService MyNavigationService { 
            get => _navigationService;
            set 
            { 
                _navigationService = value; 
                OnPropertyChanged(nameof(_navigationService)); 
            } 
        }

        public CasherVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

    }
}
