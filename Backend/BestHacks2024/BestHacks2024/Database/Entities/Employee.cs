namespace BestHacks2024.Database.Entities;

public class Employee : User
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string ExperienceLevel { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public byte[]? Image { get; set; }

    public ICollection<UserTag> UserTags { get; set; } = new List<UserTag>();
    public ICollection<Match> Matches { get; set; } = new List<Match>();
}