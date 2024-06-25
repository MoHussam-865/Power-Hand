using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Data.Repository.Invoices;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Interfaces;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureReservation
{
    public class ReservationVM : ViewModel
    {
        private IInvoicesRepo _invoicesRepo;
        private IItemsRepo _itemsRepo;

        private IEventAggregator _eventAggregator;

        private NavigationBarVM _navigationVM;
        public NavigationBarVM NavigationVM
        {
            get => _navigationVM;
            set { _navigationVM = value; OnPropertyChanged(); }
        }


        public ReservationVM(IItemsRepo itemsRepo,
            IInvoicesRepo invoicesRepo,
            IEventAggregator eventAggregator,
            NavigationBarVM navigationVM)
        {
            _invoicesRepo = invoicesRepo;
            _itemsRepo = itemsRepo;
            _eventAggregator = eventAggregator;
            _navigationVM = navigationVM;
        }

    }
}
