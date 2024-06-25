using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Features.FeatureApp.FeatureCasher;
using Power_Hand.Interfaces;

namespace Power_Hand.Features.FeatureApp.FeatureEditItem
{
    public class AddEditItemPageVM : ViewModel
    {

        private CasherItemsNavigationVM _gridItemsVM;
        public CasherItemsNavigationVM GridItemsVM
        {
            get => _gridItemsVM;
            set { _gridItemsVM = value; OnPropertyChanged(); }
        }

        private ItemFormVM _itemFormVM;
        public ItemFormVM ItemFormVM
        {
            get => _itemFormVM;
            set { ItemFormVM = value; OnPropertyChanged(); }
        }

        private NavigationBarVM _navigationVM;
        public NavigationBarVM NavigationVM
        {
            get => _navigationVM;
            set { _navigationVM = value; OnPropertyChanged(); }
        }


        ICommand NavigateBackCommand { get; set; }


        public AddEditItemPageVM(
            CasherItemsNavigationVM gridItemsVM,
            ItemFormVM itemFormVM,
            NavigationBarVM navigationBarVM)
        {
            _itemFormVM = itemFormVM;
            _gridItemsVM = gridItemsVM;
            _navigationVM = navigationBarVM;
            NavigateBackCommand = new FunCommand(OnNavigateBackClicked);
        }

        private void OnNavigateBackClicked()
        {
            // _navigationService.NavigateTo<.....> ();
        }
    }
}
