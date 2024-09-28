using MyDatabase.Models;

namespace MyDatabase.Repository.Items
{
    public interface IItemsRepo
    {
        public Task<List<Item>> GetFolders(int parentId);

        public Task<List<Item>> GetItems(int parentId);

        public Task<Item> GetItemById(int id);

        public Task<int> AddItem(Item item);

        public Task<int> UpdateItem(Item item);
        public Task<int> DeleteItem(Item item);

    }
}
