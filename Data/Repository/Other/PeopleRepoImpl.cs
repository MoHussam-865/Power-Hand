using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<Client?> GetClient(string search)
        {
            return await _database.Client.FirstOrDefaultAsync(client =>

                 (client.Name == null ? false: client.Name.Contains(search)) ||

                (client.Address == null ? false :  client.Address.Contains(search)) ||

                (client.PhoneNumber == null ? false :  client.PhoneNumber.Contains(search)) ||

                (client.Email == null ? false :  client.Email.Contains(search))
                
            );
        }

        public async Task<Emploee?> GetEmploee(string username, string password)
        {
            return await _database.Emploee.FirstOrDefaultAsync(emploee =>
                emploee.Name == username && emploee.Password == password
            );
        }
    }
}
