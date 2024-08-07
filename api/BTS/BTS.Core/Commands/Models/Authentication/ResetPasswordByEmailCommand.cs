using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Commands.Models.Authentication
{
    public sealed record ResetPasswordByEmailCommand(string Email) : IRequest<Result> { }
}
