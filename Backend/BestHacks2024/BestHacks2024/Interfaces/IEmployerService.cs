using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;

namespace BestHacks2024.Interfaces;

public interface IEmployerService
{
    public Task<IEnumerable<Employer>> GetEmployers();
    public Task<Employer?> GetEmployerById(Guid id);
    public Task<Employer?> GetNextEmployer();
    
    public Task<Employer> CreateEmployer(EmployerDto employer);
    public Task<Employer> UpdateEmployer(Guid id, EmployerDto employer);
    public Task DeleteEmployer(Guid id);
}