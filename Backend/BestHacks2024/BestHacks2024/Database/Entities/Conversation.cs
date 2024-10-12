namespace BestHacks2024.Database.Entities;

public class Conversation : BaseEntity
{
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid MatchId { get; set; }
    public Match Match { get; set; }

    public Guid SenderId { get; set; }
    public User Sender { get; set; }
}