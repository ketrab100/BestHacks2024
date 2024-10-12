namespace BestHacks2024.Database.Entities;

public class Job : BaseEntity
{
    public string JobTitle { get; set; }
    public string JobDescription { get; set; }
    public string Location { get; set; }
    public string ExperienceLevel { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid EmployerId { get; set; }
    public Employer Employer { get; set; }

    public ICollection<JobTag> JobTags { get; set; }
    public ICollection<Match> Matches { get; set; }
}