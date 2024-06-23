using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Power_Hand.DBContext;
using Power_Hand.Models;

namespace Power_Hand.Data.Repository.Items
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
            _dbContext.Item.Add(item);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateItem(Item item)
        {
            _dbContext.Item.Update(item);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
