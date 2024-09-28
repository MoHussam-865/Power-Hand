

using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;

namespace MyDatabase.Repository.Items
{
    public class ApiItemsRepoImpl(DatabaseContext databaseContext) : IApiItemsRepo
    {
        private readonly DatabaseContext _databaseContext = databaseContext;

        public async Task<List<Item>> GetItems(int lastUpdate)
        {
            return await _databaseContext.Item
                .Where(i => i.LastUpdate > lastUpdate)
                .ToListAsync();
        }

        public async Task<int> GetLastUpdate()
        {
            return await _databaseContext.Item.MaxAsync(i => i.LastUpdate);
        }
    }
}
