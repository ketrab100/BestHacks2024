namespace BestHacks2024.Database.Entities;

public class EmployerTag
{
    public Guid EmployerId { get; set; }
    public Employer Employer { get; set; }

    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}