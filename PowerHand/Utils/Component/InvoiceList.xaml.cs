using MyDatabase.Models;
using System.Windows;
using System.Windows.Controls;



namespace Power_Hand.View.Component
{
    /// <summary>
    /// Interaction logic for InvoiceList.xaml
    /// </summary

    public partial class InvoiceList : UserControl
    {



        public List<InvoiceItem> Items
        {
            get { return (List<InvoiceItem>)GetValue(MyItems); }
            set { SetValue(MyItems, value); }
        }
        public static readonly DependencyProperty MyItems =
            DependencyProperty.Register("MyProperty", typeof(int),
                typeof(UserControl), new PropertyMetadata(new List<InvoiceItem>()));



        public InvoiceList()
        {
            InitializeComponent();
        }
    }
}
