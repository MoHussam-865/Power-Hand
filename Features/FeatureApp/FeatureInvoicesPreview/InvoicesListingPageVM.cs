using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Interfaces;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureInvoicesPreview
{
    public class InvoicesListingPageVM : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        private NavigationBarVM _navigationVM;
        public NavigationBarVM NavigationVM
        {
            get => _navigationVM;
            set { _navigationVM = value; OnPropertyChanged(); }
        }

        private InvoicePreviewViewVM _previewViewVM;
        public InvoicePreviewViewVM PreviewViewVM
        {
            get => _previewViewVM;
            set { _previewViewVM = value; OnPropertyChanged(); }
        }


        private InvoicesListViewVM _listViewVM;
        public InvoicesListViewVM ListViewVM
        {
            get => _listViewVM;
            set { _listViewVM = value; OnPropertyChanged(); }
        }

        public InvoicesListingPageVM(
            IEventAggregator eventAggregator,
            InvoicePreviewViewVM previewViewVM,
            InvoicesListViewVM listViewVM,
            NavigationBarVM navigationBarVM)
        {
            _eventAggregator = eventAggregator;
            _previewViewVM = previewViewVM;
            _listViewVM = listViewVM;
            _navigationVM = navigationBarVM;
        }


    }
}
