using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;
using Power_Hand.Data.Other;

namespace Power_Hand.Features.FeatureApp.FeatureCasher
{
    /// <summary>
    /// Interaction logic for CasherView.xaml
    /// </summary>
    public partial class CasherView : UserControl
    {
        public CasherView()
        {
            InitializeComponent();
            Loaded += (_, _) => { MyCalculator.Focus(); };
        }


        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && e.OriginalSource is TextBox)
            {
                MyCalculator.Focus();
            }

            if (e.OriginalSource is not TextBox)
            {
                var x = Interaction.GetBehaviors(userControl);
                if (x.Count > 0)
                {
                    foreach (var behavior in x)
                    {
                        if (behavior is KeyPressBehavior keyPress && keyPress.Command != null && keyPress.Command.CanExecute(e))
                        {
                            keyPress.Command.Execute(e);
                            e.Handled = true;
                        }
                    }
                }
            }
        }
    }

}

