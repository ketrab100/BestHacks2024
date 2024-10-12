using BestHacks2024.Database.Entities;

namespace BestHacks2024.Dtos;

public class JobDto
{
    public string JobTitle { get; set; }
    public string JobDescription { get; set; }
    public string Location { get; set; }
    public string ExperienceLevel { get; set; }
    public Guid EmployerId { get; set; }
    public ICollection<Tag> Tags { get; set; }
}