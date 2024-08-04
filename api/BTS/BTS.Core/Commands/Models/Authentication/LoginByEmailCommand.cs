using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Commands.Models.Authentication
{
    public sealed record LoginByEmailCommand(string Email) : IRequest<Result> { }
}
