using BTS.Core.Results;
using MediatR;

namespace BTS.Core.Commands.Models.Driver.Common
{
    public interface ICreateUpdateDriverCommand : IRequest<Result>
    {
        string LicenseNo { get; init; }
        string FirstName { get; init; }
        string? MiddleName { get; init; }
        string LastName { get; init; }
        string Gender { get; init; }
        string Address { get; init; }
        string ContactNo { get; init; }
        DateTime Birthdate { get; init; }
        string Requestor { get; init; }
    }
}
