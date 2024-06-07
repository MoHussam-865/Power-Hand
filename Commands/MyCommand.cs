using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Power_Hand.Commands
{
    // not yet implemented
    public class MyCommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool>? _canExcute;
        public MyCommand(Action<object> excute, Func<object, bool>? canExcute = null) 
        {
            _canExcute = canExcute;

            _execute = excute ?? throw new ArgumentNullException(nameof(excute));
        }
    }
}
