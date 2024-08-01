using BTS.Core.Results;
using MediatR;

namespace BTS.Core.Queries.Models.Bus
{
    public record GetBusByIdQuery(Guid Id) : IRequest<Result> { }
}
