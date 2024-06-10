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
    public class RelayCommand<T> : ICommand
    {
        public delegate void ExecuteMethod();
        private Action<T> _executeMethod;
        public RelayCommand(Action<T> method) 
        {
            _executeMethod = method;                
        }

        public bool CanExecute(object? para)
        {
            return true;
        }
        public void Execute(object? para)
        {
            if (para != null)
            {
                _executeMethod((T)para);
            }
        }

        public event EventHandler? CanExecuteChanged;
       
        
    }
}
