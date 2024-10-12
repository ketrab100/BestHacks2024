namespace BestHacks2024.Dtos    
{
    public class UserRegistrationDto
    {
        public required string Email { get; set; }
        public required string Nickname { get; set; }
        public required string Password { get; set; }
        public required bool IsEmployee { get; set; }
    }
}
