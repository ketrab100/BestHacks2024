using BestHacks2024.Database.Entities;

namespace BestHacks2024.Interfaces;

public interface IMatchService
{
    public Task<ICollection<Match>> GetMatchesAsync();
    public Task<Match?> GetMatchByIdAsync(Guid id);
    public Task<ICollection<Match>> GetEmployeeMatchesAsync(Guid employeeId);
    public Task<ICollection<Match>> GetEmployerMatchesAsync(Guid employerId);
    
    public Task<Match?> CreateMatchAsync(MatchDto match);
    public Task<Match?> UpdateMatchAsync(Guid matchId, MatchDto match);
    public Task DeleteMatchAsync(Guid matchId);
}