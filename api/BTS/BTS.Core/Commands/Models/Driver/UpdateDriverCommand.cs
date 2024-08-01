using BTS.Core.Commands.Models.Driver.Common;
using BTS.Core.Results;
using BTS.Domain.Models.Dtos.Driver;
using MediatR;

namespace BTS.Core.Commands.Models.Driver
{
    public class UpdateDriverCommand : ICreateUpdateDriverCommand
    {
        public UpdateDriverCommand(Guid driverId, UpdateBusDto dto, string requestor = "System")
        {
            DriverIdToUpdate = driverId;
            LicenseNo = dto.LicenseNo;
            FirstName = dto.FirstName;
            MiddleName = dto.MiddleName;
            LastName = dto.LastName;
            Gender = dto.Gender;
            Address = dto.Address;
            ContactNo = dto.ContactNo;
            Birthdate = dto.Birthdate;
            Requestor = requestor;
        }

        public Guid DriverIdToUpdate { get; init; }
        public string LicenseNo { get; init; }
        public string FirstName { get; init; }
        public string? MiddleName { get; init; }
        public string LastName { get; init; }
        public string Gender { get; init; }
        public string Address { get; init; }
        public string ContactNo { get; init; }
        public DateTime Birthdate { get; init; }
        public string Requestor { get; init; }
    }
}
