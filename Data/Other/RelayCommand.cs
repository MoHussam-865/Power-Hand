using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Xaml.Behaviors.Core;

namespace Power_Hand.Commands
{
    // not yet implemented
    public class RelayCommand<T> : ICommand
    {
        public delegate void ExecuteMethod();
        private Action<T> _executeMethod;
        public RelayCommand(Action<T> method) => _executeMethod = method;                
        

        public bool CanExecute(object? para)=> true;
        
        public void Execute(object? para)
        {
            if(para != null) _executeMethod((T)para);
            
        }

        public event EventHandler? CanExecuteChanged;

        protected void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());
        


    }
}
