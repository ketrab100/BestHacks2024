using Microsoft.AspNetCore.Identity;

namespace BestHacks2024.Database.Entities;
public class User : IdentityUser<Guid>
{ 
    public User() { }
}
