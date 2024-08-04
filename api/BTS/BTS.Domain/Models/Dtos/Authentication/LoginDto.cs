namespace BTS.Domain.Models.Dtos.Authentication
{
    public class LoginDto
    {
        public required string UsernameOrEmail { get; set; }
        public required string Password { get; set; }
    }
}
