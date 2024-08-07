using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Commands.Models.Authentication
{
    public sealed record UpdatePasswordCommand(string Token, string NewPassword) : IRequest<Result> { }
}
