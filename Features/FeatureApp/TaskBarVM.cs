using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Features.FeatureApp.FeatureEditClient;
using Power_Hand.Features.FeatureApp.FeatureEditItem;
using Power_Hand.Features.FeatureApp.FeatureEmploee;
using Power_Hand.Features.FeatureApp.FeatureInvoicesPreview;
using Power_Hand.Features.FeatureHome;
using Power_Hand.Interfaces;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp
{
    public class TaskBarVM: ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
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

       
        public ICommand ToLoginView { get; set; }
        public ICommand ToAddEditItemsView { get; set; }
        public ICommand ToAddEditClientsView { get; set; }
        public ICommand ToInvoiceListingView { get; set; }
        public ICommand ToAddEditEmploeeCommand { get; set; }


        public TaskBarVM(
            INavigationService navigationService,
            IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            ToLoginView = new FunCommand(OnLoginViewClicked);
            ToInvoiceListingView = new FunCommand(OnInvoiceListingViewClicked);
            ToAddEditClientsView = new FunCommand(OnClientsControllViewClicked);
            ToAddEditItemsView = new FunCommand(OnItemsControllViewClicked);
            ToAddEditEmploeeCommand = new FunCommand(OnEmploeeControlViewClicked);
        }



        private void OnEmploeeControlViewClicked()
        {
            NavigationService.NavigateTo<AddEditEmploeePageVM>();
            CloseTaskBar();
        }
        private void OnItemsControllViewClicked()
        {
            NavigationService.NavigateTo<AddEditItemPageVM>();
            CloseTaskBar();
        }
        private void OnClientsControllViewClicked()
        {
            NavigationService.NavigateTo<AddEditClientPageVM>();
            CloseTaskBar();
        }
        private void OnInvoiceListingViewClicked()
        {
            NavigationService.NavigateTo<InvoicesListingPageVM>();
            CloseTaskBar();
        }
        private void OnLoginViewClicked()
        {
            NavigationService.SetParentView<HomeVM>();
            CloseTaskBar();
        }
    
        private void CloseTaskBar()
        {
            _eventAggregator.GetEvent<OpenTaskBarChannel>().Publish(false);
        }

    }
}
