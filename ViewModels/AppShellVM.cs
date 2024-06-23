using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Data.SharedData;
using Power_Hand.Interfaces;
using Prism.Events;

namespace Power_Hand.ViewModels
{
    public class AppShellVM : ViewModel
    {

        private NavigationBarVM _navigationVM;
        public NavigationBarVM NavigationVM
        {
            get => _navigationVM;
            set
            {
                _navigationVM = value;
                OnPropertyChanged();
            }
        }

        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
            set { _navigationService = value; OnPropertyChanged(); }

        }

        public AppShellVM(
            NavigationBarVM navigationBar,
            IEventAggregator eventAggregator)
        {
            _navigationVM = navigationBar;
            _navigationService = _navigationVM.NavigationService;
            eventAggregator.GetEvent<NavigationShare>().Subscribe(OnViewChanged);
        }

        private void OnViewChanged(int obj)
        {
            if (NavigationService.PopupView != null|| NavigationService.CurrentView != null)
            {
                OnPropertyChanged(nameof(NavigationService));
            }
        }
    }
}
