namespace BTS.Domain.Models.Dtos.Authentication
{
    public class LoginByCredentialsDto
    {
        public required string UsernameOrEmail { get; set; }
        public required string Password { get; set; }
    }
}
