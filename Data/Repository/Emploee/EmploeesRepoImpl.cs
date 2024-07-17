using Microsoft.EntityFrameworkCore;
using Power_Hand.Data.Models;
using Power_Hand.Models;

namespace Power_Hand.Data.Repository.Other
{
    public class EmploeesRepoImpl(DatabaseContext database) : IEmploeeRepo
    {
        private readonly DatabaseContext _database = database;

        #region Emploee
        public Employee? GetEmploee(string username, string password)
        {
            return _database.Emploee.FirstOrDefault(emploee =>
                emploee.Name == username && emploee.Password == password
            );
        }

        public async Task<int> AddEmploee(Employee emploee)
        {
            _database.Emploee.Add(emploee);
            return await _database.SaveChangesAsync();
        }

        public async Task<int> UpdateEmploee(Employee emploee)
        {
            _database.Emploee.Update(emploee);
            return await _database.SaveChangesAsync();
        }

        public async Task<int> DeleteEmploee(Employee emploee)
        {
            _database.Emploee.Remove(emploee);
            return await _database.SaveChangesAsync();
        }

        #endregion
    }
}
