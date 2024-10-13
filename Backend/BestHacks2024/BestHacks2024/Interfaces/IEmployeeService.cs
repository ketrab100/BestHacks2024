using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;

namespace BestHacks2024.Interfaces;

public interface IEmployeeService
{
    public Task<IEnumerable<Employee>> GetEmployeesAsync();
    public Task<Employee?> GetEmployeeByIdAsync(Guid id);
    public Task<List<Employee>> GetNextEmployeesAsync(Guid id, CancellationToken cancellationToken);
    
    public Task<Employee?> CreateEmployeeAsync(EmployeeDto employee);
    public Task<Employee?> UpdateEmployeeAsync(Guid id, EmployeeDto employee);
    public Task DeleteEmployeeAsync(Guid id);
}