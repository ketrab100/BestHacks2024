namespace BestHacks2024.Database.Entities;

public class Employer : User
{
    public string CompanyName { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string JobTitle { get; set; }
    public string JobDescription { get; set; }
    public string Location { get; set; }
    public string ExperienceLevel { get; set; }
    public byte[]? Image { get; set; }
    public ICollection<Match> Matches { get; set; }
    public ICollection<EmployerTag> EmployerTags { get; set; }
}