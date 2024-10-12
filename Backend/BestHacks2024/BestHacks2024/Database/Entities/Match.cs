namespace BestHacks2024.Database.Entities;

public class Match : BaseEntity
{
    public bool DidEmployeeAcceptJobOffer { get; set; }
    public bool DidEmployerAcceptCandidate { get; set; }
    public bool AreMatched { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid UserId { get; set; }
    public Employee Employee { get; set; }

    public Guid JobId { get; set; }
    public Employer Employer { get; set; }

    public ICollection<Conversation> Conversations { get; set; }
}