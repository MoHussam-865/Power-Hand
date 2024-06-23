using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Power_Hand.DBContext;
using Power_Hand.Models;

namespace Power_Hand.Data.Repository.Other
{
    public class ClientsRepoImpl(DatabaseContext database) : IClientRepo
    {
        private readonly DatabaseContext _database = database;


        #region Client
        public async Task<List<Client>?> SearchClients(string search)
        {
            return await _database.Client.Where(client => 

                 (client.Name != null && client.Name.Contains(search)) ||

                (client.Address != null && client.Address.Contains(search)) ||

                (client.PhoneNumber != null && client.PhoneNumber.Contains(search)) ||

                (client.Email != null && client.Email.Contains(search))

            ).ToListAsync();
        }
        public async Task<int> AddClient(Client client)
        {
            _database.Client.Add(client);
            return await _database.SaveChangesAsync();
        }
        public async Task<int> UpdateClient(Client client)
        {
            _database.Client.Update(client);
            return await _database.SaveChangesAsync();
        }
        public async Task<int> DeleteClient(Client client)
        {
            _database.Client.Remove(client);
            return await _database.SaveChangesAsync();
        }

        #endregion

    }
}
