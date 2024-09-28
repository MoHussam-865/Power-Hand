
using MyDatabase.Models;

namespace MyDatabase.Repository.Items
{
    public interface IApiItemsRepo
    {
        public Task<List<Item>> GetItems(int lastUpdate);
        public Task<int> GetLastUpdate();
    }
}
