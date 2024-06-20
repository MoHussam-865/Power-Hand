using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Power_Hand.Models;

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
