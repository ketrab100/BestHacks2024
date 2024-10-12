using BestHacks2024.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BestHacks2024.Database
{
    public class BestHacksDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserTag> UserTags { get; set; }
        public DbSet<EmployerTag> EmployerTags { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Conversation> Conversations { get; set; }

        public BestHacksDbContext(DbContextOptions<BestHacksDbContext> options) : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserTag>()
                .HasKey(ut => new { ut.UserId, ut.TagId });

            builder.Entity<UserTag>()
                .HasOne(ut => ut.User)
                .WithMany(e => e.UserTags)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserTag>()
                .HasOne(ut => ut.Tag)
                .WithMany(t => t.UserTags)
                .HasForeignKey(ut => ut.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EmployerTag>()
                .HasKey(et => new { et.EmployerId, et.TagId });

            builder.Entity<Match>()
                .HasOne(m => m.Employee)
                .WithMany(e => e.Matches)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Conversation>()
                .HasOne(c => c.Match)
                .WithMany(m => m.Conversations)
                .HasForeignKey(c => c.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Conversation>()
                .HasOne(c => c.Sender)
                .WithMany()
                .HasForeignKey(c => c.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
