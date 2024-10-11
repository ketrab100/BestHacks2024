using System.ComponentModel.DataAnnotations;

namespace BestHacks2024.Database.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
