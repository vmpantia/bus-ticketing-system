using BTS.Core.Results;
using MediatR;

namespace BTS.Core.Queries.Models.Driver
{
    public record GetDriversQuery : IRequest<Result> { }
}
