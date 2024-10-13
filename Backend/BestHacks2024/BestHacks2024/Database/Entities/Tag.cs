using System.Linq.Expressions;

namespace BestHacks2024.Database.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; }

    public ICollection<UserTag> UserTags { get; set; } = new List<UserTag>();
    public ICollection<EmployerTag> EmployerTags { get; set; } = new List<EmployerTag>();
}