using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Interfaces;

namespace Power_Hand.ViewModels
{
    public class AddEditItemPageVM : ViewModel
    {

        private GridItems_SVM _gridItemsVM;
        public GridItems_SVM GridItemsVM
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
            GridItems_SVM gridItemsVM,
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
