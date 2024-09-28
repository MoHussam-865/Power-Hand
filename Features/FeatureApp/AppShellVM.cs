using System.Windows;
using Power_Hand.Features.Popups;
using Power_Hand.Other.Other;
using Power_Hand.Other.SharedData;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp
{
    public class AppShellVM : ViewModel
    {
        private TaskBarVM _taskBarVM;
        public TaskBarVM MyTaskBar
        {
            get { return _taskBarVM; }
            set { _taskBarVM = value; OnPropertyChanged(); }
        }



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

        private Visibility _taskBarVisibility;

        public Visibility TaskBarVisibility
        {
            get => _taskBarVisibility;
            set { _taskBarVisibility = value;  OnPropertyChanged(); }
        }


        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
            set { _navigationService = value; OnPropertyChanged(); }

        }

        private Visibility _popupVisibility;
        public Visibility PopupVisibility
        {
            get => _popupVisibility;
            set { _popupVisibility = value; OnPropertyChanged(); }
        }

        public AppShellVM(
            NavigationBarVM navigationBar,
            IEventAggregator eventAggregator,
            TaskBarVM taskBarVM)
        {
            _navigationVM = navigationBar;
            _taskBarVM = taskBarVM;
            _taskBarVisibility = Visibility.Collapsed;
            _navigationService = _navigationVM.NavigationService;
            eventAggregator.GetEvent<NavigationShare>().Subscribe(OnViewChanged);
            eventAggregator.GetEvent<PopupCloseChannel>().Subscribe(OnPopupNeededStateChanges);
            eventAggregator.GetEvent<OpenTaskBarChannel>().Subscribe(OnTaskBarVisibilityChange);
        }

        private void OnTaskBarVisibilityChange(bool visible)
        {
            TaskBarVisibility = visible? Visibility.Visible : Visibility.Collapsed;
        }

        private void OnPopupNeededStateChanges(bool close)
        {
            PopupVisibility = !close? Visibility.Visible : Visibility.Collapsed;
            OnViewChanged();
        }

        private void OnViewChanged()
        {
            if (NavigationService.PopupView != null || NavigationService.CurrentView != null)
            {
                OnPropertyChanged(nameof(NavigationService));
            }
        }
    }
}
