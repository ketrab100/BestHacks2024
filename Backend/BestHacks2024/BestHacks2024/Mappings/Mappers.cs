using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;

namespace BestHacks2024.Mappings
{
    public static class Mappers
    {
        public static EmployerDto MapToEmployerDto(Employer employer)
        {
            return new EmployerDto
            {
                Id = employer.Id,
                CompanyName = employer.CompanyName,
                Email = employer.Email,
                Location = employer.Location,
                JobTitle = employer.JobTitle,
                JobDescription = employer.JobDescription,
                ExperienceLevel = employer.ExperienceLevel,
                ImageBase64 = Convert.ToBase64String(employer.Image ?? new byte[0]),
                Tags = employer.EmployerTags.Select(x => x.Tag).Select(MapToTagDto).ToList() // Mapping tags using the helper method
            };
        }

        public static MatchDto MapToMatchDto(Match match)
        {
            return new MatchDto
            {
                Id = match.Id,
                CreatedAt = match.CreatedAt,
                Employee = match.Employee != null ? MapToEmployeeDto(match.Employee) : null,
                Employer = match.Employer != null ? MapToEmployerDto(match.Employer) : null,
                Conversations = match.Conversations?.Select(MapToConversationDto).ToList() ?? new List<ConversationDto>()
            };
        }

        public static ConversationDto MapToConversationDto(Conversation conversation)
        {
            return new ConversationDto
            {
                Id = conversation.Id,
                Message = conversation.Message,
                CreatedAt = conversation.CreatedAt,
                AuthorId = conversation.SenderId
            };
        }

        public static EmployeeDto MapToEmployeeDto(Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Bio = employee.Bio,
                Location = employee.Location,
                Experience = employee.ExperienceLevel,
                Email = employee.Email ?? string.Empty,
                ImageBase64 = Convert.ToBase64String(employee.Image ?? new byte[0]),
                Tags = employee.UserTags.Select(x => x.Tag).Select(MapToTagDto).ToList() // Mapping tags using the helper method
            };
        }

        public static TagDto MapToTagDto(Tag tag)
        {
            return new TagDto
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }
    }
}
