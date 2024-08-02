using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Commands.Models.User
{
    public sealed record LoginUserCommand(string UsernameOrEmail, string Password) : IRequest<Result> { }
}
