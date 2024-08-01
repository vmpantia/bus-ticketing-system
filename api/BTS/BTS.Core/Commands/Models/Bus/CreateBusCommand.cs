using BTS.Core.Commands.Models.Bus.Common;
using BTS.Domain.Models.Dtos.Bus;

namespace BTS.Core.Commands.Models.Bus
{
    public class CreateBusCommand : ICreateUpdateBusCommand
    {
        public CreateBusCommand(CreateBusDto dto, string requestor = "System")
        {
            DriverId = dto.DriverId;
            RouteId = dto.RouteId;
            BusNo = dto.BusNo;
            PlateNo = dto.PlateNo;
            Make = dto.Make;
            Model = dto.Model;
            Year = dto.Year;
            Requestor = requestor;
        }

        public Guid? DriverId { get; init; }
        public Guid? RouteId { get; init; }
        public string BusNo { get; init; }
        public string PlateNo { get; init; }
        public string Make { get; init; }
        public string Model { get; init; }
        public string Year { get; init; }
        public string Requestor { get; init; }
    }
}
