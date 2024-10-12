using BestHacks2024.Database.Entities;

namespace BestHacks2024.Dtos;

public class EmployeeProfileDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Bio { get; set; }
    public string? Location { get; set; }
    public string? Experience { get; set; }
    public ICollection<TagDto> Tags { get; set; } = new List<TagDto>();
}