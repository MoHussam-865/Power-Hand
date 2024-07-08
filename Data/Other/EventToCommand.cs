using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace Power_Hand.Data.Other
{
    /// <summary>
    /// this used for barcode reader it trigers action on enter key press which
    /// mark the end of a barcode by barcode scanner
    /// </summary>
    public class EventToCommand : TriggerAction<DependencyObject>
    {

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(EventToCommand));


        // Command Parameter
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(EventToCommand));


        public bool PassArgsToCommand
        {
            get { return (bool)GetValue(PassArgsToCommandProperty); }
            set { SetValue(PassArgsToCommandProperty, value); }
        }
        public static readonly DependencyProperty PassArgsToCommandProperty =
            DependencyProperty.Register(nameof(PassArgsToCommand), typeof(bool), typeof(EventToCommand),
                new PropertyMetadata(false));




        protected override void Invoke(object parameter)
        {
            if (Command == null) return;

            var commandParameters = CommandParameter;

            if (PassArgsToCommand)
            {
                commandParameters = parameter;
            }

            if (Command.CanExecute(commandParameters))
            {
                Command.Execute(commandParameters);
            }
        }
    }
}
