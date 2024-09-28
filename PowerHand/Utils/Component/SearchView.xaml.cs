using System;
using System.Collections.Generic;
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

namespace Power_Hand.Utils.Component
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {



        public DataTemplate MyDataTemplate
        {
            get { return (DataTemplate)GetValue(MyDataTemplateProperty); }
            set { SetValue(MyDataTemplateProperty, value); }
        }
        public static readonly DependencyProperty MyDataTemplateProperty =
            DependencyProperty.Register(nameof(MyDataTemplate), typeof(DataTemplate), typeof(UserControl), new PropertyMetadata(null));



        public SearchView()
        {
            InitializeComponent();
        }
    }
}
