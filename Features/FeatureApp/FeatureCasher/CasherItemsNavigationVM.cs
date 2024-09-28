using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;
using Power_Hand.Other.Other;
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
    public class CasherItemsNavigationVM : ViewModel
    {
        private string _path = "";
        public string CurrentPath
        {
            get { return _path; }
            set { _path = value; OnPropertyChanged(); }
        }

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
            IEventAggregator eventAggregator,
            GridItemsNavigationLogic itemsNavigationVM,
            GridFoldersNavigationLogic folderNavigationVM)
        {
            _itemsNavigationVM = itemsNavigationVM;
            _folderNavigationVM = folderNavigationVM;

            eventAggregator.GetEvent<CurrentPathChannel>().Subscribe(OnPathChanged);
        }

        private void OnPathChanged(string obj) => CurrentPath = obj;
        
}
}
