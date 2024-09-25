using System.Windows.Controls;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;
using Power_Hand.Models;
using Power_Hand.Utils.ViewModels;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureCasher
{
    /// <summary>
    /// inherit from ___ which gets products from different categories
    /// and it inherit from GridPaginationLogic Responsable for pagination between pages of items
    /// in the same category
    /// 
    /// </summary>
    public class CasherItemsNavigationVM
    {

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

        public CasherItemsNavigationVM(
            GridItemsNavigationLogic itemsNavigationVM,
            GridFoldersNavigationLogic folderNavigationVM)
        {
            _itemsNavigationVM = itemsNavigationVM;
            _folderNavigationVM = folderNavigationVM;
        }

    }
}
