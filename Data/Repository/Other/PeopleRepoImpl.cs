using Microsoft.EntityFrameworkCore;
using Power_Hand.DBContext;
using Power_Hand.Models;

namespace Power_Hand.Data.Repository.Other
{
    public class PeopleRepoImpl : IPeopleRepo
    {
        private readonly DatabaseContext _database;
        public PeopleRepoImpl(DatabaseContext database)
        {
            _database = database;
        }

        public Client? GetClient(string search)
        {
            return _database.Client.FirstOrDefault(client =>

                 (client.Name != null && client.Name.Contains(search)) ||

                (client.Address != null && client.Address.Contains(search)) ||

                (client.PhoneNumber != null && client.PhoneNumber.Contains(search)) ||

                (client.Email != null && client.Email.Contains(search))

            );
        }

        public Emploee? GetEmploee(string username, string password)
        {
            return _database.Emploee.FirstOrDefault(emploee =>
                emploee.Name == username && emploee.Password == password
            );
        }
    }
}
