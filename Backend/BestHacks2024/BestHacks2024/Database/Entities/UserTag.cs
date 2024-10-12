namespace BestHacks2024.Database.Entities;

public class UserTag
{
    public Guid UserId { get; set; }
    public Employee User { get; set; }

    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}