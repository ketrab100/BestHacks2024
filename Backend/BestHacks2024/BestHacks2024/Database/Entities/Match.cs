namespace BestHacks2024.Database.Entities;

public class Match : BaseEntity
{
    public decimal MatchScore { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid UserId { get; set; }
    public Employee Employee { get; set; }

    public Guid JobId { get; set; }
    public Job Job { get; set; }

    public ICollection<Conversation> Conversations { get; set; }
}