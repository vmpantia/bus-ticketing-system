using BTS.Core.Results;
using MediatR;

namespace BTS.Core.Commands.Models.Bus.Common
{
    public interface ICreateUpdateBusCommand : IRequest<Result>
    {
        Guid? DriverId { get; init; }
        string BusNo { get; init; }
        string PlateNo { get; init; }
        string Make { get; init; }
        string Model { get; init; }
        string Year { get; init; }
        string Requestor { get; init; }
    }
}
