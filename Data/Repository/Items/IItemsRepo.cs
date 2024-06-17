using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Models;

namespace Power_Hand.Data.Repository.Items
{
    public interface IItemsRepo
    {
        public Task<List<Item>> GetFolders(int parentId);

        public Task<List<Item>> GetItems(int parentId);

        public Task<Item> GetItemById(int id);


    }
}
