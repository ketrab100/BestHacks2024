using BestHacks2024.Database;
using BestHacks2024.Database.Entities;
using BestHacks2024.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BestHacks2024.Services;

public class EmployerService : IEmployerService
{
    private readonly BestHacksDbContext _context;

    public EmployerService(BestHacksDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Employer>> GetEmployersAsync()
    {
        return await _context
            .Employers
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Employer?> GetEmployerByIdAsync(Guid id)
    {
        return await _context
            .Employers
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Employer?> GetNextEmployer()
    {
        throw new NotImplementedException();
    }

    public async Task<Employer> CreateEmployer(EmployerDto employer)
    {
        throw new NotImplementedException();
    }

    public async Task<Employer> UpdateEmployer(Guid id, EmployerDto employer)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteEmployer(Guid id)
    {
        throw new NotImplementedException();
    }
}