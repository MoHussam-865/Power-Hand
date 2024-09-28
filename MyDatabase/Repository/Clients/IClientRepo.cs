using MyDatabase.Models;

namespace MyDatabase.Repository.Clients
{
    public interface IClientRepo
    {

        public Task<List<Client>?> SearchClients(string? search);

        public Task<int> AddClient(Client client);
        public Task<int> UpdateClient(Client client);
        public Task<int> DeleteClient(Client client);
    }
}
