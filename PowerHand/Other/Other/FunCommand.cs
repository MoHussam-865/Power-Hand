
namespace Power_Hand.Other.Other
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
