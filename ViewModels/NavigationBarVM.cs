using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.SharedData;
using Power_Hand.Interfaces;
using Prism.Events;

namespace Power_Hand.ViewModels
{
    public class NavigationBarVM: ViewModel
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


        public ICommand ToCasherView {  get; set; }
        public ICommand ToLoginView { get; set; }
        public ICommand ToReservationView { get; set; }
        public ICommand ToAddEditItemsView { get; set; }
        public ICommand ToAddEditClientsView { get; set; }
        public ICommand ToInvoiceListingView { get; set; }


        public NavigationBarVM(
            INavigationService navigationService,
            IEventAggregator eventAggregator) { 
            
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            ToCasherView = new FunCommand(OnCasherViewClicked);
            ToReservationView = new FunCommand(OnReservationViewClicked);
            ToLoginView = new FunCommand(OnLoginViewClicked);
            ToInvoiceListingView = new FunCommand(OnInvoiceListingViewClicked);
            ToAddEditClientsView = new FunCommand(OnClientsControllViewClicked);
            ToAddEditItemsView = new FunCommand(OnItemsControllViewClicked);
        }

        private void OnItemsControllViewClicked() => NavigationService.NavigateTo<AddEditItemPageVM>();
        
        private void OnClientsControllViewClicked() => NavigationService.NavigateTo<AddEditClientPageVM>();
        
        private void OnInvoiceListingViewClicked() => NavigationService.NavigateTo<InvoicesListingPageVM>();
        
        private void OnLoginViewClicked() => NavigationService.NavigateTo<HomeVM>();
        
        private void OnReservationViewClicked() => NavigationService.NavigateTo<ReservationVM>();
        
        private void OnCasherViewClicked() => NavigationService.NavigateTo<CasherVM>();
        
        private void CurrentViewChanged()
        {
            _eventAggregator.GetEvent<NavigationShare>().Publish(0);
        }

    }
}
