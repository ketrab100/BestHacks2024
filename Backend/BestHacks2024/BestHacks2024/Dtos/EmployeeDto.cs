namespace BestHacks2024.Dtos;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Bio { get; set; }
    public string Location { get; set; }
    public string Experience { get; set; }
    public string Email { get; set;}
    public string ImageBase64 { get; set; }
    public ICollection<TagDto> Tags { get; set; }
    
    
}