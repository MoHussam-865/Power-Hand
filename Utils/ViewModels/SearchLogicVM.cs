using System.Collections.ObjectModel;
using Power_Hand.Interfaces;

namespace Power_Hand.Utils.ViewModels
{
    public abstract  class SearchLogicVM<T> : ViewModel
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

        public SearchLogicVM() => GetItems();
        
        
        public async void GetItems()
        {
            List<T> results = await OnSearchChanged(_search);
            SearchItems = new ObservableCollection<T>(results);
        }

        public abstract Task<List<T>> OnSearchChanged(string? search);
    }
}