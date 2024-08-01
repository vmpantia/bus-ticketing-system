using BTS.Core.Results;
using MediatR;

namespace BTS.Core.Queries.Models.Bus
{
    public record GetBusesQuery : IRequest<Result> { }
}
