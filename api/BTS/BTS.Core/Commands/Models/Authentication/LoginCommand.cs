using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Commands.Models.Authentication
{
    public sealed record LoginCommand(string UsernameOrEmail, string Password) : IRequest<Result> { }
}
