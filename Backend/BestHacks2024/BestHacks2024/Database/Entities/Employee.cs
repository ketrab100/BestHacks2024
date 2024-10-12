namespace BestHacks2024.Database.Entities;

public class Employee : User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Bio { get; set; }
    public string Location { get; set; }
    public string ExperienceLevel { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<UserTag> UserTags { get; set; }
    public ICollection<Match> Matches { get; set; }
}