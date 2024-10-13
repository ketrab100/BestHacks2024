using BestHacks2024.Database.Entities;

namespace BestHacks2024;

public class StaticMockDataGenerator
{
    public static List<Employee> GenerateEmployees(int count)
    {
        var seniority = new List<string>{ "Junior", "Mid", "Senior" };
        var employees = new List<Employee>();
        for (int i = 0; i < count; i++)
        {
            employees.Add(new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = $"FirstName{i}",
                LastName = $"LastName{i}",
                Bio = $"Experienced in various technologies. Skillset includes C#, SQL, and TypeScript.",
                Location = $"Location{i}",
                ExperienceLevel = $"{seniority[i % 3]}",
                CreatedAt = DateTime.UtcNow.AddDays(-i * 10),
                Image = null, // No image in mock data
                UserTags = new List<UserTag>
                {
                    new UserTag { TagId = Guid.NewGuid(), Tag = new Tag { Id = Guid.NewGuid(), Name = "Azure" }},
                    new UserTag { TagId = Guid.NewGuid(), Tag = new Tag { Id = Guid.NewGuid(), Name = "C#" }},
                    new UserTag { TagId = Guid.NewGuid(), Tag = new Tag { Id = Guid.NewGuid(), Name = "SQL" }}
                }
            });
        }
        return employees;
    }

    public static List<Employer> GenerateEmployers(int count)
    {
        var seniority = new List<string>{ "Junior", "Mid", "Senior" };
        var employers = new List<Employer>();
        for (int i = 0; i < count; i++)
        {
            employers.Add(new Employer
            {
                Id = Guid.NewGuid(),
                CompanyName = $"CompanyName{i}",
                ContactName = $"ContactName{i}",
                JobTitle = $"Software Engineer Position",
                JobDescription = "Looking for skilled developers in backend technologies.",
                Location = $"City{i}",
                ExperienceLevel = $"{seniority[i % 3]}",
                CreatedAt = DateTime.UtcNow.AddDays(-i * 20),
                Image = null, // No image in mock data
                EmployerTags = new List<EmployerTag>
                {
                    new EmployerTag { TagId = Guid.NewGuid(), Tag = new Tag { Id = Guid.NewGuid(), Name = "ASP.NET" }},
                    new EmployerTag { TagId = Guid.NewGuid(), Tag = new Tag { Id = Guid.NewGuid(), Name = "Azure" }},
                    new EmployerTag { TagId = Guid.NewGuid(), Tag = new Tag { Id = Guid.NewGuid(), Name = "C#" }},
                }
            });
        }
        return employers;
    }

    public static List<Match> GenerateMatches(List<Employee> employees, List<Employer> employers)
    {
        var matches = new List<Match>();
        for (int i = 0; i < Math.Min(employees.Count, employers.Count); i++)
        {
            matches.Add(new Match
            {
                Id = Guid.NewGuid(),
                Employee = employees[i],
                UserId = employees[i].Id,
                Employer = employers[i],
                JobId = employers[i].Id,
                AreMatched = true,
                CreatedAt = DateTime.UtcNow,
                DidEmployeeAcceptJobOffer = true,
                DidEmployerAcceptCandidate = true,
                Conversations = new List<Conversation>
                {
                    new Conversation
                    {
                        Id = Guid.NewGuid(),
                        Message = "Hello, I’m interested in the position.",
                        CreatedAt = DateTime.UtcNow.AddHours(-5),
                        MatchId = Guid.NewGuid(),
                        SenderId = employees[i].Id,
                        Sender = employees[i]
                    },
                    new Conversation
                    {
                        Id = Guid.NewGuid(),
                        Message = "Thank you for your interest, let’s schedule an interview.",
                        CreatedAt = DateTime.UtcNow.AddHours(-3),
                        MatchId = Guid.NewGuid(),
                        SenderId = employers[i].Id,
                        Sender = employers[i]
                    }
                }
            });
        }
        return matches;
    }
}