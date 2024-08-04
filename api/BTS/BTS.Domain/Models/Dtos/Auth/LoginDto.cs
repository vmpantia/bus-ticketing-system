namespace BTS.Domain.Models.Dtos.Auth
{
    public class LoginDto
    {
        public required string UsernameOrEmail { get; set; }
        public required string Password { get; set; }
    }
}
