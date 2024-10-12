using BestHacks2024.Database.Entities;

namespace BestHacks2024.Dtos;

public class EmployeeDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Bio { get; set; }
    public string Location { get; set; }
    public string Experience { get; set; }
    public string Email { get; set;}
    public ICollection<Tag> Tags { get; set; }
}