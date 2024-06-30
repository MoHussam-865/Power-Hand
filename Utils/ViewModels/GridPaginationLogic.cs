using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Interfaces;

namespace Power_Hand.Utils.ViewModels
{
    /// <summary>
    /// This is an abstract class encapsulate the logic for pagination between Pages of grid
    /// when the items to display are more than the page can fit when that happen it show the pagination buttons
    /// (next page button & previous page button)
    /// 
    /// this class is implemented for displaying list of 
    /// Clients ,Emploees
    /// or Items this last is different as the class that inharet from this class is an abstract class also that 
    /// encapsulate the logic for navigating between categories
    /// </summary>
    /// <typeparam name="T">It can be anything from the models (Item, Client, Emploee ...)</typeparam>
    public abstract class GridPaginationLogic<T> : ViewModel
    {
        // All the items
        private ObservableCollection<T> _allItems;
        public ObservableCollection<T> AllItems
        {
            get => _allItems;
            set { _allItems = value; OnPropertyChanged(); UpdatePageItems(); }
        }

        // items shown on the grid
        private ObservableCollection<T> _currentItems;
        public ObservableCollection<T> CurrentPageItems
        {
            get => _currentItems;
            set { _currentItems = value; OnPropertyChanged(); }
        }

        // current page
        private int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            set { _currentPage = value; OnPropertyChanged(); UpdatePageItems(); }
        }


        /// <summary>
        /// The number of rows in the grid that display the list of items
        /// </summary>
        private int _rows;
        public int Rows
        {
            get => _rows;
            set { _rows = value; OnPropertyChanged(); UpdatePageItems(); }
        }
        /// <summary>
        /// The number of columns in the grid that display the list of items
        /// </summary>
        private int _columns;
        public int Columns
        {
            get => _columns;
            set { _columns = value; OnPropertyChanged(); UpdatePageItems(); }
        }


        /// <summary>
        /// Arrange the visibility of pagination components (Next Button, Previous Button and Both)
        /// </summary>
        #region Pagination Visibility

        private Visibility _isNextPageButtonVisible;
        public Visibility IsNextPageButtonVisible
        {
            get => _isNextPageButtonVisible;
            set { _isNextPageButtonVisible = value; OnPropertyChanged(); }
        }

        private Visibility _isPreviousPageButtonVisible;
        public Visibility IsPreviousPageButtonVisible
        {
            get => _isPreviousPageButtonVisible;
            set { _isPreviousPageButtonVisible = value; OnPropertyChanged(); }
        }

        private Visibility _isPaginationVisible;
        public Visibility IsPaginationVisible
        {
            get => _isPaginationVisible;
            set { _isPaginationVisible = value; OnPropertyChanged(); }
        }
        #endregion


        public ICommand PrevoiusPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }

        // Constructor
        public GridPaginationLogic()
        {
            _rows = GetRows();
            _columns = GetColumns();
            _allItems = [];
            _currentItems = [];
            PrevoiusPageCommand = new FunCommand(OnPrevoiusClicked);
            NextPageCommand = new FunCommand(OnNextclicked);

            _isPaginationVisible = GetVisibility();
        }

        /// <summary>
        /// set visibility to the pagination elements (buttons)
        /// </summary>
        private Visibility GetVisibility()
        {
            IsNextPageButtonVisible = CurrentPage < TotalPages()? Visibility.Visible : Visibility.Hidden;
            IsPreviousPageButtonVisible = CurrentPage > 1 ? Visibility.Visible : Visibility.Hidden;
            return (TotalPages() > 1) ? Visibility.Visible : Visibility.Hidden;
        }

        // when next page button clicked
        private void OnNextclicked()
        {
            if (CurrentPage < TotalPages())
            {
                CurrentPage = _currentPage + 1;
            }
        }

        // when previous page button clicked
        private void OnPrevoiusClicked()
        {
            if (CurrentPage > 1)
            {
                CurrentPage = _currentPage - 1;
            }
        }

        /// <summary>
        /// given that each page has x number of rows and y number of columns 
        /// it calculate the number of pages needed to display all the items in the list called (AllItems)
        /// </summary>
        /// <returns> number of pages needed </returns>
        private int TotalPages()
        {
            int itemsPerPage = _rows * _columns;
            return (int)Math.Ceiling((double)AllItems.Count / itemsPerPage);
        }

        /// <summary>
        /// whenever the page changes it gets the right items that corespond to that page
        /// </summary>
        private void UpdatePageItems()
        {
            int itemsPerPage = _rows * _columns;
            IEnumerable<T> items = AllItems.Skip((CurrentPage - 1) * itemsPerPage).Take(itemsPerPage);
            CurrentPageItems = new ObservableCollection<T>(items);
            IsPaginationVisible = GetVisibility();
        }


        /// <summary>
        /// this method will be used to update the items if needed
        /// 
        /// like for example when we are displaying the products and frequently change the categore and 
        /// therefore changeing the list of products
        /// 
        /// the page will be reset to 1
        /// </summary>
        /// <param name="items"></param>
        public virtual void OnAllItemsChanged(ObservableCollection<T> items)
        {
            AllItems = new ObservableCollection<T>(items);
            CurrentPage = 1;
        }


        /// <summary>
        /// That methods are used to get "ItemsPerPage" constant
        /// that equal to rows * columns
        /// </summary>
        /// <returns></returns>
        public abstract int GetRows();
        public abstract int GetColumns();


    }
}
