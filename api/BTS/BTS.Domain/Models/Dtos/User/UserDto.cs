namespace BTS.Domain.Models.Dtos.User
{
    public class UserDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }

        // Access Control
        public required bool IsEmailConfirmed { get; set; }
        public required bool IsAdmin { get; set; }
        public required string Status { get; set; }

        public required DateTimeOffset LastUpdateAt { get; set; }
        public required string LastUpdateBy { get; set; }
    }
}
