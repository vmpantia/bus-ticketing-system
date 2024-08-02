namespace BTS.Domain.Models.Dtos.User
{
    public class LoginUserDto
    {
        public required string UsernameOrEmail { get; set; }
        public required string Password { get; set; }
    }
}
