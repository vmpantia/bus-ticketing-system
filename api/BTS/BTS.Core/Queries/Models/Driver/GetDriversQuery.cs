using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Queries.Models.Driver
{
    public record GetDriversQuery : IRequest<Result> { }
}
