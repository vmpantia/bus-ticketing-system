using BTS.Core.Results;
using MediatR;

namespace BTS.Core.Queries.Models.Driver
{
    public record GetDriverByIdQuery(Guid Id) : IRequest<Result> { }
}
