using System.Diagnostics;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Data.SharedData;
using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;
using Power_Hand.Features.FeatureApp.FeatureEditItem.Channels;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Microsoft.Win32;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureEditItem
{
    public class ItemFormVM : ViewModel
    {
        // to observe shared data
        private readonly IEventAggregator _eventAggregator;
        // to make database operation (add, edit)
        private readonly IItemsRepo _itemsRepo;
        // current item that gets updated (is null if we are adding new item no item selected)
        private Item? _currentItem;
        // item will be added to it
        private Item? _currentFolder;
        // get previous shared data
        private readonly SharedValuesStore _appStore;

        #region Properties
        private string? _name;
        public string? Name { get => _name; set { _name = value; OnPropertyChanged(); } }

        private string? _description;
        public string? Description { get => _description; set { _description = value; OnPropertyChanged(); } }

        private string? _price;
        public string? Price { get => _price; set { _price = value; OnPropertyChanged(); } }

        //public int ParentId { get; set; }
        private bool _isDeleted = false;
        public bool IsDeleted { get => _isDeleted; set { _isDeleted = value; OnPropertyChanged(); } }

        private bool _isFolder = false;
        public bool IsFolder { get => _isFolder; set { _isFolder = value; OnPropertyChanged(); } }

        private string? _expence;
        public string? Expence { get => _expence; set { _expence = value; OnPropertyChanged(); } }

        private string? _note;
        public string? Note { get => _note; set { _note = value; OnPropertyChanged(); } }

        private string? _discount;
        public string? Discount { get => _discount; set { _discount = value; OnPropertyChanged(); } }

        private string _imagePath = "";
        public string ImagePath
        {
            get => _imagePath;
            set { _imagePath = value; OnPropertyChanged(); }
        }

        #endregion

        // commands
        public ICommand OnSaveCommand { get; set; }
        public ICommand OnDiscardCommand { get; set; }
        public ICommand OnDeleteCommand { get; set; }
        public ICommand ChooseImageCommand { get; set; }

        // constructor
        public ItemFormVM(
            IEventAggregator eventAggregator,
            IItemsRepo itemsRepo,
            SharedValuesStore store)
        {
            _eventAggregator = eventAggregator;
            _itemsRepo = itemsRepo;
            OnSaveCommand = new FunCommand(OnSaveClicked);
            OnDiscardCommand = new FunCommand(OnDiscardClicked);
            OnDeleteCommand = new FunCommand(OnDeleteClicked);
            ChooseImageCommand = new FunCommand(OnImageChooseClicked);

            _appStore = store;
            // observe items that get selected for edit
            _currentFolder = _appStore.SharedFolder;
            _eventAggregator.GetEvent<EditItemCurrentFolderShareChannel>().Subscribe(OnParentPublished);
            _eventAggregator.GetEvent<EditSelectedItemShareChannel>().Subscribe(OnItemPublished);
            // if item is selected we fill its data
            FillIfCan();
        }

        private void OnImageChooseClicked()
        {
            OpenFileDialog dialog = new();

            if (dialog.ShowDialog() == true)
            {
                ImagePath = dialog.FileName;
            }

        }


        #region Observe Values
        // observe current folder
        private void OnParentPublished(Item? folder) => _currentFolder = folder;

        // observe shared (selected) item
        private void OnItemPublished(Item? item)
        {
            _currentItem = item;
            FillIfCan();
        }

        #endregion

        #region Add Edit Delete Cancel
        private void OnDeleteClicked()
        {
            IsDeleted = true;
            OnSaveClicked();
        }

        private void OnDiscardClicked()
        {
            Clear();
        }

        private async void OnSaveClicked()
        {

            int currentFolderId = _currentFolder == null ? 0 : _currentFolder.Id;

            // adding Folders logic
            /*if (!string.IsNullOrEmpty(Name) && IsFolder)
            {
                Item item = new(id:0, name:Name,
                     parent: parentId,isFolder:true, price:0);
                
                await _itemsRepo.AddItem(item);
                _eventAggregator.GetEvent<ItemDatabaseUpdated>().Publish();
                Clear();
            }*/

            // Parsing values
            double price;
            double expence;
            double discount;

            #region Handel Parseing values
            try
            {
                price = string.IsNullOrEmpty(Price) ? 0 : double.Parse(Price);
            }
            catch
            {
                Debug.WriteLine("AddEditItems: Error while converting price");
                throw;
            }

            try
            {
                expence = string.IsNullOrEmpty(Expence) ? 0 : double.Parse(Expence);
            }
            catch
            {
                Debug.WriteLine("AddEditItems: Error while converting expence");
                throw;
            }

            try
            {
                discount = string.IsNullOrEmpty(Discount) ? 0 : double.Parse(Discount);
            }
            catch
            {
                Debug.WriteLine("AddEditItems: Error while converting discount");
                throw;
            }
            #endregion

            // adding item logic
            if (!string.IsNullOrEmpty(Name) && price > 0 && _currentFolder != null)
            {
                int id = _currentItem == null ? 0 : _currentItem.Id;
                Item myItem = new(id: id, name: Name,
                        price: price, parent: currentFolderId,
                        description: Description,
                        expence: expence, note: Note,
                        isDeleted: IsDeleted,
                        discount: discount, imagePath: ImagePath);

                if (_currentItem == null)
                {
                    await _itemsRepo.AddItem(myItem);
                }
                else
                {
                    await _itemsRepo.UpdateItem(myItem);
                }
                _eventAggregator.GetEvent<EditItemDatabaseUpdatedChannel>().Publish(typeof(EditItemsGridVM));
                Clear();
            }


        }

        #endregion



        private void FillIfCan()
        {
            Name = _currentItem?.Name;
            Description = _currentItem?.Description;
            Price = _currentItem?.Price.ToString();
            IsDeleted = _currentItem != null && _currentItem.IsDeleted;
            IsFolder = _currentItem != null && _currentItem.IsFolder;
            Expence = _currentItem?.Expence.ToString();
            Note = _currentItem?.Note;
            Discount = _currentItem?.Discount.ToString();
        }

        // clear everything
        private void Clear()
        {
            _currentItem = null;
            Name = null;
            Description = null;
            Price = null;
            IsDeleted = false;
            IsFolder = false;
            Expence = null;
            Note = null;
            Discount = null;
            ImagePath = null;
            _eventAggregator.GetEvent<EditSelectedItemShareChannel>().Publish(_currentItem);
        }
    }
}
