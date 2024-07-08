using System.Windows;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Features.FeatureApp.FeatureCasher;
using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;
using Power_Hand.Features.FeatureApp.FeatureEditClient;
using Power_Hand.Features.FeatureApp.FeatureEditItem;
using Power_Hand.Features.FeatureApp.FeatureEmploee;
using Power_Hand.Features.FeatureApp.FeatureInvoicesPreview;
using Power_Hand.Features.FeatureApp.FeatureReservation;
using Power_Hand.Features.FeatureHome;
using Power_Hand.Interfaces;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp
{
    public class NavigationBarVM : ViewModel
    {
        /// <summary>
        /// handles the tool bar at the top that is responsible of navigation between view (sections)
        /// </summary>
        private readonly IEventAggregator _eventAggregator;
        private bool _calculatorVisibility = false;
        private bool _taskBarVisibility = false;
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

        public ICommand CalculatorCommand { get; set; }
        public ICommand ToCasherView { get; set; }
        public ICommand ToReservationView { get; set; }
        public ICommand ToInvoiceListingView { get; set; }
        public ICommand OpenTaskBarCommand { get; set; }

        public NavigationBarVM(
            INavigationService navigationService,
            IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            OpenTaskBarCommand = new FunCommand(OnTaskBarOpen);
            ToCasherView = new FunCommand(OnCasherViewClicked);
            ToReservationView = new FunCommand(OnReservationViewClicked);
            ToInvoiceListingView = new FunCommand(OnInvoiceListingViewClicked);
            CalculatorCommand = new FunCommand(OnCalculatorSelected);
            _eventAggregator.GetEvent<OpenTaskBarChannel>().Subscribe(TaskBarVisibilityChanged);
        }

        private void TaskBarVisibilityChanged(bool obj) => _taskBarVisibility = obj;

        private void OnTaskBarOpen()
        {
            _taskBarVisibility = !_taskBarVisibility;
            _eventAggregator.GetEvent<OpenTaskBarChannel>().Publish(_taskBarVisibility);
        }
        private void OnCalculatorSelected()
        {
            _calculatorVisibility = !_calculatorVisibility;
            _eventAggregator.GetEvent<CalculatorVisibilityChannel>().Publish(_calculatorVisibility);
        }

        private void OnInvoiceListingViewClicked() => NavigationService.NavigateTo<InvoicesListingPageVM>();

        private void OnReservationViewClicked() => NavigationService.NavigateTo<ReservationVM>();

        private void OnCasherViewClicked() => NavigationService.NavigateTo<CasherVM>();

        

    }
}
