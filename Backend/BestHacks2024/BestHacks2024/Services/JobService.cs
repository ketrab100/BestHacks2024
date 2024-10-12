using BestHacks2024.Database;
using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BestHacks2024.Services;

public class JobService : IJobService
{
    private readonly BestHacksDbContext _context;

    public JobService(BestHacksDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Job>> GetJobsAsync()
    {
        return await _context
            .Jobs
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Job?> GetJobAsync(Guid jobId)
    {
        return await _context
            .Jobs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == jobId);
    }

    public async Task<Job?> GetNextJobAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Job> AddJobAsync(JobDto job)
    {
        throw new NotImplementedException();
    }

    public async Task<Job> UpdateJobAsync(Guid jobId, JobDto job)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteJobAsync(Guid jobId)
    {
        throw new NotImplementedException();
    }
}