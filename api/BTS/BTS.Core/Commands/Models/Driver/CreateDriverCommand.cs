using BTS.Core.Commands.Models.Driver.Common;
using BTS.Domain.Results;
using BTS.Domain.Models.Dtos.Driver;
using MediatR;

namespace BTS.Core.Commands.Models.Driver
{
    public class CreateDriverCommand : ICreateUpdateDriverCommand
    {
        public CreateDriverCommand(CreateDriverDto dto, string requestor = "System")
        {
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
