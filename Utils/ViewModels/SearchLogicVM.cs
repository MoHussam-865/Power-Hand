using System.Collections.ObjectModel;
using System.Windows.Input;
using Power_Hand.Other.Other;

namespace Power_Hand.Utils.ViewModels
{
    public abstract class SearchLogicVM<T> : ViewModel
    {
        private string? _search;
        public string? Search
        {
            get => _search;
            set { _search = value; OnPropertyChanged(); GetItems(); }
        }

        private ObservableCollection<T> _searchItems = [];
        public ObservableCollection<T> SearchItems
        {
            get => _searchItems;
            set { _searchItems = value; OnPropertyChanged(); }
        }

        public ICommand ItemClickCommand { get; set; }

        public SearchLogicVM()
        {
            ItemClickCommand = new ClickCommand<T>((i) => OnItemClicked(i));
        }     

        public async void GetItems()
        {
            List<T> results = await OnSearchChanged(_search);
            SearchItems = new ObservableCollection<T>(results);
        }

        public abstract Task<List<T>> OnSearchChanged(string? search);
        public abstract void OnItemClicked(T? item);

    }
}