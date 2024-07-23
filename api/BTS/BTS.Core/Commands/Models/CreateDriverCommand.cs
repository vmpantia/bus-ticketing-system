using BTS.Domain.Models.Dtos.Driver;
using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Commands.Models
{
    public record CreateDriverCommand(CreateDriverDto Request, string Requestor = "System") : IRequest<Result> { }
}
