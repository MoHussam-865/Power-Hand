using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Power_Hand.Data.Other
{
    /** Any command that takes to args */
    public class FunCommand : CommandBase
    {
        private Action _loging;

        public FunCommand(Action login) => _loging = login;
        public override void Execute(object? parameter)
        {
            _loging();
        }
    }
}
