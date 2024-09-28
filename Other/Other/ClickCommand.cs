namespace Power_Hand.Other.Other
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
