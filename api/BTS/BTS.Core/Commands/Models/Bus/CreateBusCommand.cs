using BTS.Core.Results;
using BTS.Domain.Models.Dtos.Bus;
using MediatR;

namespace BTS.Core.Commands.Models.Bus
{
    public class CreateBusCommand : IRequest<Result>
    {
        public CreateBusCommand(CreateBusDto dto, string requestor = "System")
        {
            DriverId = dto.DriverId;
            BusNo = dto.BusNo;
            PlateNo = dto.PlateNo;
            Make = dto.Make;
            Model = dto.Model;
            Year = dto.Year;
            Requestor = requestor;
        }

        public Guid? DriverId { get; init; }
        public string BusNo { get; init; }
        public string PlateNo { get; init; }
        public string Make { get; init; }
        public string Model { get; init; }
        public string Year { get; init; }
        public string Requestor { get; init; }
    }
}
