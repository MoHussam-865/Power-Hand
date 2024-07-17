using Power_Hand.Models;

namespace Power_Hand.Data.Repository.Other
{
    public interface IEmploeeRepo
    {
        public Employee? GetEmploee(string username, string password);

        public Task<int> AddEmploee(Employee emploee);
        public Task<int> UpdateEmploee(Employee emploee);
        public Task<int> DeleteEmploee(Employee emploee);
    }
}
