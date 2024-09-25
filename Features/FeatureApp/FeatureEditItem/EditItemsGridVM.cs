using System.Windows.Controls;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Features.FeatureApp.FeatureEditItem.Channels;
using Power_Hand.Models;
using Power_Hand.Utils.ViewModels;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureEditItem
{
    public class EditItemsGridVM
    {
        private readonly IEventAggregator _eventAggregator;


        private GridFoldersNavigationLogic _folderNavigationVM;
        public GridFoldersNavigationLogic FolderNavigationVM
        {
            get { return _folderNavigationVM; }
            set { _folderNavigationVM = value; }
        }


        private GridItemsNavigationLogic _itemsNavigationVM;
        public GridItemsNavigationLogic ItemsNavigationVM
        {
            get { return _itemsNavigationVM; }
            set { _itemsNavigationVM = value; }
        }

        public EditItemsGridVM(IEventAggregator eventAggregator,
            GridFoldersNavigationLogic folderNavigationVM,
            GridItemsNavigationLogic itemsNavigationVM)
        {
            _eventAggregator = eventAggregator;
            _folderNavigationVM = folderNavigationVM;
            _itemsNavigationVM = itemsNavigationVM;
        }


    }
}
