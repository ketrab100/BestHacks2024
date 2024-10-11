using BestHacks2024.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BestHacks2024.Database
{
    public class BestHacksDbContext(DbContextOptions<BestHacksDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
    {
    }
}
