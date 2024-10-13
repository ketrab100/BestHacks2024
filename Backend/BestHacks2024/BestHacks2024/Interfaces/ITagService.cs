using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;

namespace BestHacks2024.Interfaces;

public interface ITagService
{
    public Task<IEnumerable<Tag>> GetTagsAsync();
    
    public Task<Tag> CreateTagAsync(TagDto tagDto);
    public Task DeleteTagAsync(Guid id);
}