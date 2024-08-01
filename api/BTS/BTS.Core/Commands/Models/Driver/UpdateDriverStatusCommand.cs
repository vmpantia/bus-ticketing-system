using BTS.Core.Results;
using BTS.Domain.Models.Enums;
using MediatR;

namespace BTS.Core.Commands.Models.Driver
{
    public sealed record UpdateDriverStatusCommand(Guid DriverId, CommonStatus NewStatus, string Requestor = "System") : IRequest<Result> { }
}
