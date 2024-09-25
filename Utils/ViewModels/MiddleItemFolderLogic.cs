using Power_Hand.Data.Other;
using Power_Hand.Features.FeatureApp.FeatureEditItem.Channels;
using Power_Hand.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Power_Hand.Utils.ViewModels
{
    public abstract class MiddleItemFolderLogic : GridPaginationLogic<Item>
    {
        // refreshes the database
        private bool _databaseChanged = false;

        public ICommand ItemClickCommand { get; set; }



        public MiddleItemFolderLogic(IEventAggregator eventAggregator)
        {
            ItemClickCommand = new ClickCommand<Item>((x) => OnItemClicked(x));
            eventAggregator.GetEvent<EditItemDatabaseUpdatedChannel>().Subscribe(OnDatabaseChanged);
        }

        // listen to database changes and refresh 
        void OnDatabaseChanged(Type me)
        {
            Type myType = this.GetType();
            if (me == myType) { _databaseChanged = true; }
        }

        public void RefreshPage(ObservableCollection<Item> items)
        {
            // Reset the items  (if database changed just refresh without changing the page)
            OnAllItemsChanged(items, _databaseChanged);
            _databaseChanged = false;
        }


        public abstract void OnItemClicked(Item item);
        public abstract int MyRows();
        public abstract int MyColums();



        /// applying inhereted members
        public override int GetColumns() => MyColums();
        public override int GetRows() => MyRows();

    }
}
