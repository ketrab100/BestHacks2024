namespace BestHacks2024.Dtos;

/// <summary>
/// Comes from Entity that SENDS the message.
/// </summary>
public class AddConversationDto
{
    public Guid MatchId { get; set; }
    public string Message { get; set; }
}