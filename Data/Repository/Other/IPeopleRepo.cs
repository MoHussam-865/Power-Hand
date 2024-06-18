using Power_Hand.Models;

namespace Power_Hand.Data.Repository.Other
{
    public interface IPeopleRepo
    {
        public Task<Emploee?> GetEmploee(string username, string password);

        public Task<Client?> GetClient(string search);
    }
}
