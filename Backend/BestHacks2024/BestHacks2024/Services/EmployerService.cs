using BestHacks2024.Database;
using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;
using Microsoft.EntityFrameworkCore;
using RestSharp;

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

    public async Task<List<Employer>> GetNextEmployers(Guid id, CancellationToken cancellationToken)
    {
        var options = new RestClientOptions($"http://localhost:8000/matches/");
        var client = new RestClient(options);
        var request = new RestRequest($"job/{id}/");
        var response = await client.GetAsync<List<EmployerAiDto>>(request, cancellationToken);
        var aiJobs = new List<EmployerAiDto>();
        
        if (response is not null)
        {
            aiJobs = response.Where(x => x.Score > 0).ToList();
        }

        var user = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        var userTagIds = user.UserTags.Select(x => x.TagId);
        var employers = await _context.Employers.Where(x => aiJobs.Select(y => y.Id).Contains(x.Id)).ToListAsync(cancellationToken);

        var restOfJobs = await _context.Employers
            .Where(x=> x.Matches.Any(y=>y.UserId!=user.Id))
            .Where(x => x.EmployerTags.Any(y => userTagIds.Contains(y.TagId))).OrderBy(x=>x.Id).Take(10-aiJobs.Count).ToListAsync(cancellationToken);

        employers = employers.Concat(restOfJobs).ToList();
        return employers;
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