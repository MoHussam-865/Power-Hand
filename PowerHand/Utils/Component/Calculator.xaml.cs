using System.Windows;
using System.Windows.Controls;

namespace Power_Hand.Utils.Component
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : UserControl
    {

        /// <summary>
        /// this used to change the visibility of the NumberPad if the user preferer
        /// to use the keyboard
        /// </summary>
        public Visibility KeyboardVisibility

        {
            get { return (Visibility)GetValue(KeyboardVisibilityProperty); }
            set { SetValue(KeyboardVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyboardVisibilityProperty =
            DependencyProperty.Register(nameof(KeyboardVisibility), typeof(Visibility), typeof(UserControl), 
                new PropertyMetadata(Visibility.Visible));


        public Calculator()
        {
            InitializeComponent();
        }

    }
}
