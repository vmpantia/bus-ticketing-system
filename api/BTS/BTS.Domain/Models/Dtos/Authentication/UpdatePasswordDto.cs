namespace BTS.Domain.Models.Dtos.Authentication
{
    public class UpdatePasswordDto
    {
        public required string Token { get; set; }
        public required string NewPassword { get; set; }
    }
}
