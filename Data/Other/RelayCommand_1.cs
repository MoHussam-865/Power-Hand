using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Power_Hand.Commands
{
    // not yet implemented
    public class RelayCommand : ICommand
    {
        public delegate void ExecuteMethod();
        private Action _executeMethod;
        public RelayCommand(Action method) => _executeMethod = method;                
        

        public bool CanExecute(object? para)
        {
            return true;
        }
        public void Execute(object? para)
        {
            if (para != null)
            {
                _executeMethod();
            }
        }

        public event EventHandler? CanExecuteChanged;

        protected void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());


    }
}
