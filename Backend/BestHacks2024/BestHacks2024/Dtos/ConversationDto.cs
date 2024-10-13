namespace BestHacks2024.Interfaces;

public class ConversationDto
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid AuthorId { get; set; }
}