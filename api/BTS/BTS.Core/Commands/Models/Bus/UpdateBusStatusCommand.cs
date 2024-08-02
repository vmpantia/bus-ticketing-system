using BTS.Domain.Results;
using BTS.Domain.Models.Enums;
using MediatR;

namespace BTS.Core.Commands.Models.Bus
{
    public sealed record UpdateBusStatusCommand(Guid BusId, CommonStatus NewStatus, string Requestor = "System") : IRequest<Result> { }
}
