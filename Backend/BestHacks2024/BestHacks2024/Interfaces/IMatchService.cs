using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;

namespace BestHacks2024.Interfaces;

public interface IMatchService
{
    public Task<ICollection<Match>> GetMatchesAsync();
    public Task<Match?> GetMatchByIdAsync(Guid id);
    public Task<ICollection<Match>> GetEmployeeMatchesAsync(Guid employeeId);
    public Task<ICollection<Match>> GetEmployerMatchesAsync(Guid employerId);
    
    public Task<Match?> CreateMatchAsync(Guid userId, SwipeDto swipeDto);
    public Task<AddConversationDto> CreateConversationAsync(Guid userId, AddConversationDto conversationDto);
    public Task<Match?> UpdateMatchAsync(Guid userId, SwipeDto swipeDto);
    public Task DeleteMatchAsync(Guid matchId);
}