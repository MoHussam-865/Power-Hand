using Microsoft.EntityFrameworkCore;
using Power_Hand.Data.Models;
using Power_Hand.Models;

namespace Power_Hand.Data.Repository.Other
{
    public class ClientsRepoImpl(DatabaseContext database) : IClientRepo
    {
        private readonly DatabaseContext _database = database;


        #region Client
        public async Task<List<Client>?> SearchClients(string search)
        {
            search = search.ToLower();
            return await _database.Client.Where(client => 

                 (client.Name != null && client.Name.ToLower().Contains(search)) ||

                (client.Address != null && client.Address.ToLower().Contains(search)) ||

                (client.PhoneNumber != null && client.PhoneNumber.ToLower().Contains(search)) ||

                (client.Email != null && client.Email.ToLower().Contains(search))

            ).ToListAsync();
        }
        public async Task<int> AddClient(Client client)
        {
            _database.Client.Add(client);
            return await _database.SaveChangesAsync();
        }
        public async Task<int> UpdateClient(Client client)
        {
            Client? myClient = await _database.Client.FindAsync(client.Id);
            if (myClient != null)
            {
                _database.Entry(myClient).State = EntityState.Detached;
            }
            _database.Client.Update(client);
            return await _database.SaveChangesAsync();
        }
        public async Task<int> DeleteClient(Client client)
        {
            Client? myClient = await _database.Client.FindAsync(client.Id);
            if (myClient != null)
            {
                _database.Entry(myClient).State = EntityState.Detached;
            }
            _database.Client.Remove(client);
            return await _database.SaveChangesAsync();
        }

        #endregion

    }
}
