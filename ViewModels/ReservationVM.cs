using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Data.Repository.Invoices;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Interfaces;
using Prism.Events;

namespace Power_Hand.ViewModels
{
    public class ReservationVM : ViewModel
    {
        private IInvoicesRepo _invoicesRepo;
        private IItemsRepo _itemsRepo;
        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
            set
            {
                _navigationService = value;
                OnPropertyChanged();
            }
        }
        private IEventAggregator _eventAggregator;

        public ReservationVM(IItemsRepo itemsRepo,
            IInvoicesRepo invoicesRepo,
            INavigationService navigationService,
            IEventAggregator eventAggregator)
        {
            _invoicesRepo = invoicesRepo;
            _itemsRepo = itemsRepo;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
        }

    }
}
