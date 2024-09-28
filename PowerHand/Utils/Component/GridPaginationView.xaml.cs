using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Power_Hand.Utils.ViewModels;

namespace Power_Hand.Utils.Component
{
    /// <summary>
    /// Interaction logic for GridPaginationLogic class (view model)
    /// its a General View that can be re-used the implementation is for making a grid of items
    /// with a specific number of rows and colums and navigate (pagination) between pages if the items to display
    /// are more than rows * columns (it display exactly rows * columns items per page)
    /// 
    /// it takes some args  (the item template of how to show each item in the grid , the items source that contains all
    /// the items to display, commands (for next page and previous page move), and visibility of pagination to hide it
    /// if the pagination is not needed (if the number of items to display is less that the items per page, it also has the 
    /// visibility of next and previous page buttons to hide if on the last page or first page respectivily and finally 
    /// it takes the number of rows and columns for each page
    /// </summary>
    public partial class GridPaginationView : UserControl
    {
        // Template
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(UserControl), new PropertyMetadata(null));

        // the Items Source
        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(UserControl), new PropertyMetadata(null));

        // previous command
        public ICommand PreviousCommand
        {
            get { return (ICommand)GetValue(PreviousCommandProperty); }
            set { SetValue(PreviousCommandProperty, value); }
        }
        public static readonly DependencyProperty PreviousCommandProperty =
            DependencyProperty.Register(nameof(PreviousCommand), typeof(ICommand), typeof(UserControl),
                new PropertyMetadata(null));

        // next command
        public ICommand NextCommand
        {
            get { return (ICommand)GetValue(NextCommandProperty); }
            set { SetValue(NextCommandProperty, value); }
        }
        public static readonly DependencyProperty NextCommandProperty =
            DependencyProperty.Register(nameof(NextCommand), typeof(ICommand), typeof(UserControl),
                new PropertyMetadata(null));



        #region Pagination Visibility
        /// <summary>
        /// visibility of the Pagination container it will be invisible if the number of items is less than the number
        /// of item for each page (means that all items are displayed on the same page and no pagination is needed
        /// </summary>
        public Visibility PaginationVisibility
        {
            get { return (Visibility)GetValue(PaginationVisibilityProperty); }
            set { SetValue(PaginationVisibilityProperty, value); }
        }
        public static readonly DependencyProperty PaginationVisibilityProperty =
            DependencyProperty.Register(nameof(PaginationVisibility), typeof(Visibility), typeof(UserControl), 
                new PropertyMetadata(Visibility.Visible));


        /// <summary>
        /// visibility of the button that goes to the previous page it will be invisible if the current page is equall
        /// to 1
        /// </summary>
        public Visibility PreviousButtonVisibility
        {
            get { return (Visibility)GetValue(PreviousButtonVisibilityProperty); }
            set { SetValue(PreviousButtonVisibilityProperty, value); }
        }
        public static readonly DependencyProperty PreviousButtonVisibilityProperty =
            DependencyProperty.Register("PreviousButtonVisibility", typeof(Visibility), typeof(UserControl),
                new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// visibility of the button that goes to the next page it will be invisible if the current page is equall
        /// to the total pages  (mean it can go no further)
        /// </summary>
        public Visibility NextPageButtonVisibility
        {
            get { return (Visibility)GetValue(NextPageButtonVisibilityProperty); }
            set { SetValue(NextPageButtonVisibilityProperty, value); }
        }
        public static readonly DependencyProperty NextPageButtonVisibilityProperty =
            DependencyProperty.Register("NextPageButtonVisibility", typeof(Visibility), typeof(UserControl), 
                new PropertyMetadata(Visibility.Visible));



        #endregion
        /// <summary>
        /// Taking the number of Rows and Columns to Pass it to the DynamicGrid (it inharite from Panel)
        /// that Panel will Arrange items in the right rows and columns
        /// </summary>
        #region Items Per Page (Rows & Columns)
        public int MyRows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register(nameof(MyRows), typeof(int), typeof(UserControl), new PropertyMetadata(0));

        public int MyColumns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register(nameof(MyColumns), typeof(int), typeof(UserControl), new PropertyMetadata(0));
        #endregion

        // Constructor
        public GridPaginationView()
        {
            InitializeComponent();
        }
    }
}
