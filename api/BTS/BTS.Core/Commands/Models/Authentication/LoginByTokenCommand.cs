using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Commands.Models.Authentication
{
    public sealed record LoginByTokenCommand(string Token) : IRequest<Result> { }
}
