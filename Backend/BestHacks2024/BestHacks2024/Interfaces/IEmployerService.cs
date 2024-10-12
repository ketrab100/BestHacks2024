using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;

namespace BestHacks2024.Interfaces;

public interface IEmployerService
{
    public Task<IEnumerable<Employer>> GetEmployersAsync();
    public Task<Employer?> GetEmployerByIdAsync(Guid id);
    public Task<List<Employer>?> GetNextEmployers(Guid id, CancellationToken cancellationToken);
    
    public Task<Employer> CreateEmployer(EmployerDto employer);
    public Task<Employer> UpdateEmployer(Guid id, EmployerDto employer);
    public Task DeleteEmployer(Guid id);
}