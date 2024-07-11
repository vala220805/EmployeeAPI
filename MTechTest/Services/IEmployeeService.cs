using MTechTest.Models;

namespace MTechTest.Services
{
    public interface IEmployeeService
    {
        Task AddEmployee(Employee employee);
        Task DeleteEmployee(int id);
        Task<Employee> GetEmployee(int id);
        Task<IEnumerable<Employee>> GetEmployees();
        Task UpdateEmployee(int id, Employee employee);
    }
}