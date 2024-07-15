using BTS.Domain.Models.Dtos.Driver;
using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Queries.Models.Driver
{
    public record GetDriversQuery : IRequest<ApiResponse<IEnumerable<DriverDto>>> { }
}
