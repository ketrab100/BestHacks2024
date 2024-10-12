using AutoMapper;
using BestHacks2024.Database;
using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BestHacks2024.Services;

public class JobService : IJobService
{
    private readonly BestHacksDbContext _context;
    private readonly IMapper _mapper;
    public JobService(BestHacksDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Job>> GetJobsAsync()
    {
        return await _context
            .Jobs
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Job?> GetJobByIdAsync(Guid jobId)
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

    public async Task<Job> AddJobAsync(JobDto jobDto)
    {
        var employer = await _context.Employers.FirstOrDefaultAsync(x => x.Id == jobDto.EmployerId);
        if (employer == null)
        {
            throw new Exception("Employer not found");
        }

        var tagIds = jobDto.Tags.Select(t => t.Id).ToList();
        var existingTags = await _context.Tags
            .Where(t => tagIds.Contains(t.Id))
            .ToListAsync();

        var job = _mapper.Map<Job>(jobDto);

        job.Employer = employer;

        job.JobTags = existingTags.Select(tag => new JobTag
        {
            TagId = tag.Id,
            Tag = tag
        }).ToList();

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();

        return job;
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