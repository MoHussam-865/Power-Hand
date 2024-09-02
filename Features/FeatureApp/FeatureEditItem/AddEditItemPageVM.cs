using Power_Hand.Interfaces;

namespace Power_Hand.Features.FeatureApp.FeatureEditItem
{
    public class AddEditItemPageVM(
        EditItemsGridVM gridItemsVM,
        ItemFormVM itemFormVM) : ViewModel
    {

        private EditItemsGridVM _gridItemsVM = gridItemsVM;
        public EditItemsGridVM GridItemsVM
        {
            get => _gridItemsVM;
            set { _gridItemsVM = value; OnPropertyChanged(); }
        }

        private ItemFormVM _itemFormVM = itemFormVM;
        public ItemFormVM ItemFormVM
        {
            get => _itemFormVM;
            set { _itemFormVM = value; OnPropertyChanged(); }
        }
    }
}
