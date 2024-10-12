namespace BestHacks2024.Database.Entities;

public class Employer : User
{
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string Location { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Job> Jobs { get; set; }
}