using MyDatabase.Models;

namespace MyDatabase.Repository.Emploee
{
    public interface IEmployeeRepo
    {
        public Employee? GetEmployee(string username, string password);
        public Task<List<Employee>?> GetEmployees(string? search = null);
        public Task<int> AddEmployee(Employee employee);
        public Task<int> UpdateEmployee(Employee employee);
        public Task<int> DeleteEmployee(Employee employee);
    }
}
