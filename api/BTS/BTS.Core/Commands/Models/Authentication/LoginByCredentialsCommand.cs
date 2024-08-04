using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Commands.Models.Authentication
{
    public sealed record LoginByCredentialsCommand(string UsernameOrEmail, string Password) : IRequest<Result> { }
}
