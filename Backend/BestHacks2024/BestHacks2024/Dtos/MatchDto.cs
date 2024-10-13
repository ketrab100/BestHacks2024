using BestHacks2024.Dtos;

namespace BestHacks2024.Interfaces;

public class MatchDto
{
    public DateTime CreatedAt { get; set; }
    public EmployeeDto? Employee { get; set; }
    public EmployerDto? Employer { get; set; }
    public ICollection<ConversationDto> Conversations {get; set;}
}