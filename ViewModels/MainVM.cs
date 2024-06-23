using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Power_Hand.Interfaces;

/*
 * when adding new view (feature) 
 * 
 * add view & view model
 * add them to App.xaml
 * add them to the App.cs (dependancy injection)
 * */




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
                OnPropertyChanged();
            }
        }

        public MainVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            MyNavigationService.SetParentView<HomeVM>();
        }

    }
}
