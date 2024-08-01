namespace BTS.Domain.Models.Dtos.Bus
{
    public class CreateBusDto
    {
        public Guid? DriverId { get; set; }
        public Guid? RouteId { get; set; }
        public required string BusNo { get; set; }
        public required string PlateNo { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public required string Year { get; set; }
    }
}
