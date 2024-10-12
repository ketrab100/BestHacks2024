using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;

namespace BestHacks2024.Interfaces;

public interface IJobService
{
    public Task<IEnumerable<Job>> GetJobsAsync();
    public Task<Job?> GetJobByIdAsync(Guid jobId);
    public Task<Job?> GetNextJobAsync();
    
    public Task<Job> AddJobAsync(JobDto job);
    public Task<Job> UpdateJobAsync(Guid jobId, JobDto job);
    public Task DeleteJobAsync(Guid jobId);
}