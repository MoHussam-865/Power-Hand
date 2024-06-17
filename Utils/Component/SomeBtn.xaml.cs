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

namespace Power_Hand.View.Component
{
    /// <summary>
    /// Interaction logic for SomeBtn.xaml
    /// </summary>
    public partial class SomeBtn : UserControl
    {


        public string MyText
        {
            get { return (string) GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register("MyText", typeof(string), typeof(UserControl), new PropertyMetadata("Hello"));



        public Color MyColor
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("MyColor", typeof(Color), typeof(UserControl), new PropertyMetadata(Colors.Black));



        public SomeBtn()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
