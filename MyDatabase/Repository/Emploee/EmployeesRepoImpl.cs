using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;

namespace MyDatabase.Repository.Emploee
{
    public class EmployeesRepoImpl(DatabaseContext database) : IEmployeeRepo
    {
        private readonly DatabaseContext _database = database;

        #region Emploee
        public Employee? GetEmployee(string username, string password)
        {
            return _database.Employee.FirstOrDefault(employee =>
                employee.Name == username && employee.Password == password
            );
        }

        public async Task<int> AddEmployee(Employee employee)
        {
            _database.Employee.Add(employee);
            return await _database.SaveChangesAsync();
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            Employee? myItem = await _database.Employee.FindAsync(employee.Id);
            if (myItem != null)
            {
                _database.Entry(myItem).State = EntityState.Detached;
            }
            _database.Employee.Update(employee);
            return await _database.SaveChangesAsync();
        }

        public async Task<int> DeleteEmployee(Employee employee)
        {
            _database.Employee.Remove(employee);
            return await _database.SaveChangesAsync();
        }

        public async Task<List<Employee>?> GetEmployees(string? search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return await _database.Employee.ToListAsync();
            }
            search = search.ToLower();
            return await _database.Employee.Where(e => (e.Name ?? "").ToLower().Contains(search)).ToListAsync();
        }

        #endregion
    }
}
