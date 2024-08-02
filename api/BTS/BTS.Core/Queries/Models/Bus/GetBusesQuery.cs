using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Queries.Models.Bus
{
    public record GetBusesQuery : IRequest<Result> { }
}
