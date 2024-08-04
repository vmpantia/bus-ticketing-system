using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Commands.Models.Auth
{
    public sealed record LoginCommand(string UsernameOrEmail, string Password) : IRequest<Result> { }
}
