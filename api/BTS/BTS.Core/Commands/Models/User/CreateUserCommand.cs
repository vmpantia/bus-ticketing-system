using BTS.Domain.Models.Dtos.User;
using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Commands.Models.User
{
    public sealed class CreateUserCommand : IRequest<Result>
    {
        public CreateUserCommand(CreateUserDto dto, bool isAdmin, string requestor = "System")
        {
            Username = dto.Username;
            Email = dto.Email;
            Password = dto.Password;
            FirstName = dto.FirstName;
            MiddleName = dto.MiddleName;
            LastName = dto.LastName;
            IsAdmin = isAdmin;
            Requestor = requestor;
        }

        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string FirstName { get; init; }
        public string? MiddleName { get; set; }
        public string LastName { get; init; }

        // Access Control
        public bool IsAdmin { get; init; }

        public string Requestor { get; init; }
    }
}
