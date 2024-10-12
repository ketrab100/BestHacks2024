namespace BestHacks2024.Database.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; }

    public ICollection<UserTag> UserTags { get; set; }
    public ICollection<JobTag> JobTags { get; set; }
}