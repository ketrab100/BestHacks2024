namespace BestHacks2024.Database.Entities;

public class JobTag
{
    public Guid JobId { get; set; }
    public Job Job { get; set; }

    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}