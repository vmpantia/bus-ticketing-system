using BTS.Domain.Contractors.Repositories.Common;
using BTS.Domain.Models.Entities;
using System.Linq.Expressions;

namespace BTS.Domain.Contractors.Repositories
{
    public interface IRouteRepository : IBaseRepository<Route>
    {
        Task<Route> GetRouteFullInfoAsync(Expression<Func<Route, bool>> expression, CancellationToken token);
        Task<IEnumerable<Route>> GetRoutesFullInfoAsync(CancellationToken token);
    }
}