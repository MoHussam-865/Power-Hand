using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;
using Power_Hand.Features.FeatureApp.FeatureEditItem.Channels;
using Power_Hand.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace Power_Hand.Utils.ViewModels
{
    public class GridFoldersNavigationLogic : MiddleItemFolderLogic
    {
        // used to pass data between view models as channels and listeners
        private readonly IEventAggregator _eventAggregator;
        // used to navigate back 
        private readonly List<Item> _currentPath = [];
        // used for database operations getting categories and products
        private readonly IItemsRepo _itemsRepo;

        public ICommand OnBackClickedCommand { get; set; }

        /// <summary>
        /// also refered as categories
        /// </summary>
        private ObservableCollection<Item> _folders;
        // folders list
        public ObservableCollection<Item> Folders
        {
            get => _folders;
            set
            {
                _folders = value;
                OnPropertyChanged();
                RefreshPage(value);
            }
        }

        private int _currentFolderId;
        public int CurrentFolderId
        {
            get => _currentFolderId;
            set { _currentFolderId = value; OnPropertyChanged(); OpenFolder(); }
        }


        public GridFoldersNavigationLogic(IItemsRepo itemsRepo,
            IEventAggregator eventAggregator): base(eventAggregator) 
        {
            _itemsRepo = itemsRepo;
            _eventAggregator = eventAggregator;

            _folders = [];
            // getting them really
            CurrentFolderId = 0;


            // initiate commands here
            OnBackClickedCommand = new FunCommand(OnGoBackClicked);
        }



        /// <summary>
        /// goes to the previous parent category (obviosly if the current category (folder) 
        /// is a sub-category of another
        /// </summary>
        private void OnGoBackClicked()
        {
            if (_currentPath.Count != 0)
            {
                // get the current folder
                Item currentFolder = _currentPath.Last();
                // remove it from the path
                _currentPath.Remove(currentFolder);
                // change the current folder id to be it's parent id
                CurrentFolderId = currentFolder.ParentId;

                // TODO Update UpdateCurrentItem
            }
        }

        
        /// <summary>
        /// when category is chosen it gets all the products in the category or open
        /// a list of all the sub categories 
        /// </summary>
        private async void OpenFolder()
        {
            if (CurrentFolderId == -1)
            {
                OnGoBackClicked();
                return;
            }

            List<Item> folders = await _itemsRepo.GetFolders(CurrentFolderId);

            if (folders.Count != 0)
            {
                if (_currentPath.Count > 0)
                {
                    Item item = new(name: "Back", id: -1, price: 0, parent: -1);
                    folders.Insert(0, item);
                }

                Folders = new ObservableCollection<Item>(folders);
            }
            _eventAggregator.GetEvent<FolderIdChangedChannel>().Publish(CurrentFolderId);
        }

        


        /// <summary>
        /// rows and columns of the folder grid
        /// </summary>
        /// <returns></returns>

        public override void OnItemClicked(Item item)
        {
            var myItem = _currentPath.LastOrDefault();

            if (myItem?.ParentId == item.ParentId)
            {
                if (myItem != null) _currentPath.Remove(myItem);
            }
            _currentPath.Add(item);

            CurrentFolderId = item.Id;
            _eventAggregator.GetEvent<CasherFolderShareChannel>().Publish(item);
        }

        public override int MyRows() => 1;
        public override int MyColums() => 4;
    }
}
