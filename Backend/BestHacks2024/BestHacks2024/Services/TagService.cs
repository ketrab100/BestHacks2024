using BestHacks2024.Database;
using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;
using BestHacks2024.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BestHacks2024.Services;

public class TagService : ITagService
{
    private readonly BestHacksDbContext _context;

    public TagService(BestHacksDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tag>> GetTagsAsync()
    {
        return _context
            .Tags
            .AsNoTracking()
            .ToList();
    }

    public async Task<Tag> CreateTagAsync(TagDto tagDto)
    {
        var tag = new Tag()
        {
            Name = tagDto.Name,
            EmployerTags = new List<EmployerTag>(),
            UserTags = new List<UserTag>()
        };
        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();
        return tag;
    }

    public async Task DeleteTagAsync(Guid id)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

        if (tag == null)
        {
            throw new Exception("Tag not found");
        }

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();
    }
}