using BestHacks2024.Database.Entities;

namespace BestHacks2024;

public class StaticMockDataGenerator
{
    private static Random random = new Random();

    // Generate random tags to be used for both Employees and Employers
    public static List<Tag> GenerateTags(int count)
    {
        var tagNames = new List<string>
        {
            "C# Developer", "Full-Stack", "Cloud Architect", "Front-End", "Back-End", 
            "JavaScript", "Machine Learning", "AI Specialist", "DevOps", "Cybersecurity",
            "Database Administrator", "Mobile Development", "Blockchain", "UI/UX", "Project Manager"
        };

        var tags = new List<Tag>();
        for (int i = 0; i < count; i++)
        {
            tags.Add(new Tag
            {
                Id = Guid.NewGuid(),
                Name = tagNames[i % tagNames.Count]
            });
        }
        return tags;
    }

    private static List<UserTag> GenerateRandomUserTags(Employee user, List<Tag> availableTags, int minTags, int maxTags)
    {
        var userTags = new List<UserTag>();
        int tagCount = random.Next(minTags, maxTags + 1);
        var chosenTags = availableTags.OrderBy(x => random.Next()).Take(tagCount).ToList();

        foreach (var tag in chosenTags)
        {
            userTags.Add(new UserTag
            {
                UserId = user.Id,
                User = user,
                TagId = tag.Id,
                Tag = tag
            });
        }
        return userTags;
    }

    private static List<EmployerTag> GenerateRandomEmployerTags(Employer employer, List<Tag> availableTags, int minTags, int maxTags)
    {
        var employerTags = new List<EmployerTag>();
        int tagCount = random.Next(minTags, maxTags + 1);
        var chosenTags = availableTags.OrderBy(x => random.Next()).Take(tagCount).ToList();

        foreach (var tag in chosenTags)
        {
            employerTags.Add(new EmployerTag
            {
                EmployerId = employer.Id,
                Employer = employer,
                TagId = tag.Id,
                Tag = tag
            });
        }
        return employerTags;
    }

    public static List<Employee> GenerateEmployees(int count, List<Tag> availableTags)
    {
        var seniority = new List<string> { "Junior", "Mid", "Senior" };
        var names = new List<string> { "John", "Paul", "Cris", "Daniel", "Ann", "Cassandra" };
        var surnames = new List<string> { "Doe", "Johnson", "Harrington", "Voidtyla", "Ashford" };
        var bios = new List<string>
        {
            "Passionate software engineer with experience in full-stack development and cloud architecture.",
            "Seasoned developer with a strong background in mobile app development and UX design.",
            "Experienced data scientist with a focus on machine learning and AI-driven solutions.",
            "Full-stack developer with a love for building scalable web applications and microservices.",
            "Front-end specialist skilled in React, TypeScript, and modern JavaScript frameworks.",
            "DevOps engineer with expertise in CI/CD pipelines, containerization, and cloud infrastructure.",
            "Backend developer with a passion for writing clean, efficient code in C# and Python.",
            "Cybersecurity expert with a focus on protecting applications and networks from threats.",
            "Database administrator experienced in managing large-scale relational and NoSQL databases.",
            "Game developer with a love for creating immersive experiences in Unity and Unreal Engine."
        };
        var locations = new List<string>
        {
            "Warsaw, Poland",
            "Kraków, Poland",
            "Gdańsk, Poland",
            "Wrocław, Poland",
            "Poznań, Poland",
            "Łódź, Poland",
            "Szczecin, Poland",
            "Lublin, Poland",
            "Katowice, Poland",
            "Białystok, Poland"
        };

        var employees = new List<Employee>();
        Random r = new Random();
        for (int i = 0; i < count; i++)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = $"{names[r.Next() % names.Count]}",
                LastName = $"{surnames[r.Next() % surnames.Count]}",
                Bio = $"{bios[r.Next() % bios.Count]}",
                Location = $"{locations[r.Next() % locations.Count]}",
                ExperienceLevel = $"{seniority[i % 3]}",
                CreatedAt = DateTime.UtcNow.AddDays(-i * 10),
                Image = null, // No image in mock data
                Email = $"employee{i}@example.com",
                UserName = $"employee{i}@example.com"
            };

            var userTags = GenerateRandomUserTags(employee, availableTags, 1, 3);
            employee.UserTags = employee.UserTags.Concat(userTags).ToList();

            employees.Add(employee);
        }
        return employees;
    }

    public static List<Employer> GenerateEmployers(int count, List<Tag> availableTags)
    {
        var seniority = new List<string> { "Junior", "Mid", "Senior" };
        var employers = new List<Employer>();
        var companyNames = new List<string>
        {
            "TechInnovators Ltd.",
            "GlobalSoft Solutions",
            "FutureTech Systems",
            "NextGen Enterprises",
            "Quantum Dynamics",
            "Skyline IT Services",
            "BlueWave Technologies",
            "Pinnacle Software Group",
            "Visionary WebWorks",
            "Apex Data Solutions"
        };

        Random r = new Random();
        for (int i = 0; i < count; i++)
        {
            var employer = new Employer
            {
                Id = Guid.NewGuid(),
                CompanyName = $"{companyNames[r.Next() % companyNames.Count]}",
                ContactName = $"ContactName{i}",
                JobTitle = $"Software Engineer Position",
                JobDescription = "Looking for skilled developers in backend technologies.",
                Location = $"City{i}",
                ExperienceLevel = $"{seniority[i % 3]}",
                CreatedAt = DateTime.UtcNow.AddDays(-i * 20),
                Image = null, // No image in mock data
                Email = $"employer{i}@example.com",
                UserName = $"employer{i}@example.com"
            };

            // Assign random tags to the employer
            var employerTags = GenerateRandomEmployerTags(employer, availableTags, 1, 3);
            employer.EmployerTags = employer.EmployerTags.Concat(employerTags).ToList();

            employers.Add(employer);
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
