using Microsoft.EntityFrameworkCore;
using MTechTest.Context;
using MTechTest.Models;
using System.Globalization;
using System.Text;

namespace MTechTest.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeContext _employeeContext;
        private readonly RfcValidator _rfcValidator;

        public EmployeeService(EmployeeContext employeeContext, RfcValidator rfcValidator)
        {
            _employeeContext = employeeContext;
            _rfcValidator = rfcValidator;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeeContext.Employees.OrderBy(bd => bd.BornDate).ToListAsync();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _employeeContext.Employees.FindAsync(id);
        }

        public async Task AddEmployee(Employee employee)
        {
            bool IsRFCValid = _rfcValidator.ValidateRfc(employee);
            bool IsRFCUnique = _rfcValidator.CheckUniqueRfc(employee.RFC, _employeeContext);

            if (IsRFCUnique && IsRFCValid)
            {
                _employeeContext.Employees.Add(employee);
                await _employeeContext.SaveChangesAsync();
            }
        }

        public async Task UpdateEmployee(int id, Employee employee)
        {
            var employeeToUpdate = await _employeeContext.Employees.FindAsync(id);
            bool IsRFCValid = _rfcValidator.ValidateRfc(employee);
            bool IsRFCUnique = _rfcValidator.CheckUniqueRfc(employee.RFC, _employeeContext);

            if (employeeToUpdate != null && IsRFCUnique && IsRFCValid)
            {
                employeeToUpdate.Name = employee.Name;
                employeeToUpdate.LastName = employee.LastName;
                employeeToUpdate.RFC = employee.RFC;
                employeeToUpdate.BornDate = employee.BornDate;
                employeeToUpdate.Status = employee.Status;

                _employeeContext.Employees.Update(employeeToUpdate);
                await _employeeContext.SaveChangesAsync();
            }
        }

        public async Task DeleteEmployee(int id)
        {
            var employeeToDelete = await GetEmployee(id);

            if (employeeToDelete != null)
            {
                _employeeContext.Employees.Remove(employeeToDelete);
                await _employeeContext.SaveChangesAsync();
            }
        }

        
    }
}
