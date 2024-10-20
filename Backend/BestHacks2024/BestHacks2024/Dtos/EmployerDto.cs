using BestHacks2024.Dtos;

namespace BestHacks2024.Interfaces;

public class EmployerDto
{
    public Guid Id { get; set; }
    public string? CompanyName { get; set; }
    public string? Email { get; set; }
    public string? Location { get; set; }
    public string? JobTitle { get; set; }
    public string? JobDescription { get; set; }
    public string? ExperienceLevel { get; set; }
    public string? ImageBase64 { get; set; }
    public ICollection<TagDto> Tags { get; set; } 
}