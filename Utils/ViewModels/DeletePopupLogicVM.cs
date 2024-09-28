using System.Windows.Input;
using Power_Hand.Features.Popups;
using Power_Hand.Other.Other;

namespace Power_Hand.Utils.ViewModels
{
    public abstract class DeletePopupLogicVM<T> : ViewModel
    {

        public abstract string Title { get; }
        public abstract string Message { get; }
        public abstract T? ThingToDelete { get; }
        public abstract void OnDelete(T thingToDelete);
        public abstract void OnCancel();


        private readonly IEventAggregator _eventAggregator;
        public ICommand DeleteCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private void OnClose()
        {
            OnCancel();
            CloseWindow();
        }

        private void TakeAction()
        {
            if (ThingToDelete != null) { OnDelete(ThingToDelete); }
            CloseWindow(); 
        }



        public DeletePopupLogicVM(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            DeleteCommand = new FunCommand(TakeAction);
            CancelCommand = new FunCommand(OnClose);
        }

        private void CloseWindow()
        {
            _eventAggregator.GetEvent<PopupCloseChannel>().Publish(true);
        }

    }
}
