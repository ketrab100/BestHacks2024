using BestHacks2024.Database;
using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;
using BestHacks2024.Migrations;
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
        var aiJobs = new List<EmployerAiDto>();
        try
        {
            var options = new RestClientOptions($"http://localhost:8000/matches/");
            var client = new RestClient(options);
            var request = new RestRequest($"employers/{id}/");
            var response = await client.GetAsync<List<EmployerAiDto>>(request, cancellationToken);
            if (response is not null)
            {
                aiJobs = response.Where(x => x.Score > 0).ToList();
            }
        }
        catch{}

        var user = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        var userTagIds = user.UserTags.Select(x => x.TagId);
        var employers = await _context.Employers.Where(x => aiJobs.Select(y => y.Id).Contains(x.Id)).ToListAsync(cancellationToken);

        var restOfJobs = await _context.Employers
            .Where(x=> x.Matches.Any(y=>y.UserId!=user.Id))
            .Where(x => x.EmployerTags.Any(y => userTagIds.Contains(y.TagId)))
            .OrderBy(x=>x.Id).Take(10-aiJobs.Count).ToListAsync(cancellationToken);
        
        employers = employers.Concat(restOfJobs).ToList();
        return employers;
    }

    public async Task<Employer> CreateEmployerAsync(EmployerDto employerDto)
    {
        var tagIds = employerDto.Tags.Select(t => t.Id).ToList();
        var existingTags = await _context.Tags
            .Where(t => tagIds.Contains(t.Id))
            .ToListAsync();

        var employer = new Employer()
        {
            CompanyName = employerDto.CompanyName,
            ContactName = employerDto.Email,
            JobTitle = employerDto.JobTitle,
            JobDescription = employerDto.JobDescription,
            Location = employerDto.Location,
            ExperienceLevel = employerDto.ExperienceLevel,
            Image = Convert.FromBase64String(employerDto.ImageBase64 ?? ""),
        };
        
        employer.CreatedAt = DateTime.UtcNow;

        employer.EmployerTags = existingTags.Select(tag => new EmployerTag()
        {
            Tag = tag,
            Employer = employer
        }).ToList();

        var newEmployer = _context.Employers.Add(employer);
        var employerTags = employerDto.Tags.Select(x => new EmployerTag() { TagId = x.Id, EmployerId = newEmployer.Entity.Id });
        await _context.EmployerTags.AddRangeAsync(employerTags);
        await _context.SaveChangesAsync();

        return newEmployer.Entity;
    }

    public async Task<Employer> UpdateEmployerAsync(Guid id, EmployerDto employerDto)
    {
        var employer = await _context.Employers
            .Include(e => e.EmployerTags)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employer == null)
            throw new KeyNotFoundException("Employee not found");
        
        var employerTags = employerDto.Tags.Select(x => new EmployerTag { TagId = x.Id, EmployerId = employer.Id});
        employer.EmployerTags = employerTags.ToList();
        
        await _context.SaveChangesAsync();
        return employer;
    }

    public async Task DeleteEmployer(Guid id)
    {
        throw new NotImplementedException();
    }
}