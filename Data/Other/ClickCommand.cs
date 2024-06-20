using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Models;

namespace Power_Hand.Data.Other
{
    /** Any command that takes item as an input  */
    public class ClickCommand<T> : CommandBase
    {
        private Action<T> _loging;

        public ClickCommand(Action<T> login) => _loging = login;
        public override void Execute(object? parameter)
        {
            if (parameter != null)
            {
                _loging((T)parameter);
            }
        }
    }
}
