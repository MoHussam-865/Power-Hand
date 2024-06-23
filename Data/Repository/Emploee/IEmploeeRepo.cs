using Power_Hand.Models;

namespace Power_Hand.Data.Repository.Other
{
    public interface IEmploeeRepo
    {
        public Emploee? GetEmploee(string username, string password);

        public Task<int> AddEmploee(Emploee emploee);
        public Task<int> UpdateEmploee(Emploee emploee);
        public Task<int> DeleteEmploee(Emploee emploee);
    }
}
