using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using MyDatabase.Repository.Settings;

namespace MyDatabase.Repository.Items
{


    public class ItemsRepoImpl(DatabaseContext database) : IItemsRepo
    {
        private readonly DatabaseContext _dbContext = database;

        public async Task<List<Item>> GetFolders(int parentId)
        {
            return await _dbContext.Item.Where((item) =>
            item.ParentId == parentId && item.IsFolder && !item.IsDeleted).ToListAsync();
        }

        public async Task<List<Item>> GetItems(int parentId)
        {
            return await _dbContext.Item.Where((item) =>
            item.ParentId == parentId && !item.IsFolder && !item.IsDeleted).ToListAsync();
        }

        public async Task<Item> GetItemById(int id)
        {
            return await _dbContext.Item.FirstAsync(item => item.Id == id);
        }

        public async Task<int> AddItem(Item item)
        {
            item.LastUpdate = SettingsRepo.GetLastUpdate();

            _dbContext.Item.Add(item);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateItem(Item item)
        {
            Item? myItem = await _dbContext.Item.FindAsync(item.Id);
            if (myItem != null)
            {
                _dbContext.Entry(myItem).State = EntityState.Detached;
            }

            item.LastUpdate = SettingsRepo.GetLastUpdate();

            _dbContext.Item.Update(item);
            return await _dbContext.SaveChangesAsync();
        }


        public async Task<int> DeleteItem(Item item)
        {
            item.IsDeleted = true;
            return await UpdateItem(item);
        }

    }
}
