using Power_Hand.Interfaces;

namespace Power_Hand.Data.Other
{
    public interface INavigationService
    {
        ViewModel CurrentView { get; }
        ViewModel ParentView { get; }
        ViewModel? PopupView { get; }
        void NavigateTo<T>() where T : ViewModel;
        void SetParentView<T>() where T : ViewModel;
        void OpenPopup<T>() where T : ViewModel;
        void ClosePopup();

    }



    public class NavigationService(Func<Type, ViewModel> viewModelFactory) : ViewModel, INavigationService
    {
        private readonly Func<Type, ViewModel> _viewModelFactory = viewModelFactory;

        private ViewModel _currentView;
        public ViewModel CurrentView { get => _currentView; set { _currentView = value; OnPropertyChanged(); } }

        private ViewModel _parentView;
        public ViewModel ParentView { get => _parentView; set { _parentView = value; OnPropertyChanged(); } }

        private ViewModel? _popupView;
        public ViewModel? PopupView { get => _popupView; set { _popupView = value; OnPropertyChanged(); } }




        public void NavigateTo<TViewModel>() where TViewModel : ViewModel
        {
            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }

        public void SetParentView<TViewModel>() where TViewModel : ViewModel
        {
            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            ParentView = viewModel;
        }

        public void OpenPopup<TViewModel>() where TViewModel : ViewModel
        {
            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            PopupView = viewModel;
        }

        public void ClosePopup() => PopupView = null;
    }
}
