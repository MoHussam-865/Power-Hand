using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Data.SharedData;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.ViewModels
{
    public class ItemFormVM : ViewModel
    {
        private IEventAggregator _eventAggregator;
        private IItemsRepo _itemsRepo;

        private Item? _currentItem;
        private Item? _currentFolder;

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


        public ICommand OnSaveCommand { get; set; }
        public ICommand OnDiscardCommand { get; set; }
        public ICommand OnDeleteCommand { get; set; }


        public ItemFormVM(
            IEventAggregator eventAggregator,
            IItemsRepo itemsRepo)
        {
            _eventAggregator = eventAggregator;
            _itemsRepo = itemsRepo;
            OnSaveCommand = new FunCommand(OnSaveClicked);
            OnDiscardCommand = new FunCommand(OnDiscardClicked);
            OnDeleteCommand = new FunCommand(OnDeleteClicked);

            // get item if Edit
            _eventAggregator.GetEvent<ItemShare>().Subscribe(OnItemPublished);
            _eventAggregator.GetEvent<FolderShare>().Subscribe(OnParentPublished);

            FillIfCan();
        }

        private void FillIfCan()
        {
            if (_currentItem != null)
            {
                _name = _currentItem.Name;
                _description = _currentItem.Description;
                _price = _currentItem.Price.ToString();
                _isDeleted = _currentItem.IsDeleted;
                _isFolder = _currentItem.IsFolder;
                _expence = _currentItem.Expence.ToString();
                _note = _currentItem.Note;
                _discount = _currentItem.Discount.ToString();
            }
        }

        private void OnParentPublished(Item? folder)
        {
            _currentFolder = folder;
        }

        private void OnItemPublished(Item? item)
        {
            _currentItem = item;
            FillIfCan();
        }

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

            double price;
            double expence;
            double discount;

            try
            {
                price = Price == null ? 0 : double.Parse(Price);
            }
            catch
            {
                Debug.WriteLine("AddEditItems: Error while converting price");
                throw;
            }

            try
            {
                expence = Expence == null ? 0 : double.Parse(Expence);
            }
            catch
            {
                Debug.WriteLine("AddEditItems: Error while converting expence");
                throw;
            }

            try
            {
                discount = Discount == null ? 0 : double.Parse(Discount);
            }
            catch
            {
                Debug.WriteLine("AddEditItems: Error while converting discount");
                throw;
            }

            if (Name != null && price > 0 && _currentFolder != null)
            {
                int id = _currentItem == null ? 0 : _currentItem.Id;


                Item myItem = new(id: id, name: Name,
                        price: price, parent: _currentFolder.Id,
                        description: Description,
                        expence: expence, note: Note,
                        discount: discount);

                if (_currentItem == null)
                {
                    await _itemsRepo.AddItem(myItem);
                }
                else
                {
                    await _itemsRepo.UpdateItem(myItem);
                }
                Clear();
            }
        }


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
            _eventAggregator.GetEvent<ItemShare>().Publish(_currentItem);
        }
    }
}
