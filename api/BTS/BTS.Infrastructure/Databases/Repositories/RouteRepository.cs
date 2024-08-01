using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Entities;
using BTS.Infrastructure.Databases.Contexts;
using BTS.Infrastructure.Databases.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BTS.Infrastructure.Databases.Repositories
{
    public class RouteRepository : BaseRepository<Route>, IRouteRepository
    {
        public RouteRepository(BTSDbContext context) : base(context) { }

        public async Task<IEnumerable<Route>> GetRoutesFullInfoAsync(CancellationToken token)
        {
            // Get all route stored in the database
            var entities = await GetAllAsync(token);
            var result = await entities.Include(tbl => tbl.Bus)
                                       .Include(tbl => tbl.OriginTerminal)
                                       .Include(tbl => tbl.DestinationTerminal)
                                       .AsSplitQuery()
                                       .ToListAsync(token);

            return result;
        }

        public async Task<Route> GetRouteFullInfoAsync(Expression<Func<Route, bool>> expression, CancellationToken token)
        {
            // Get route stored in the database
            var entities = await GetByExpressionAsync(expression, token);
            var result = await entities.Include(tbl => tbl.Bus)
                                       .Include(tbl => tbl.OriginTerminal)
                                       .Include(tbl => tbl.DestinationTerminal)
                                       .AsSplitQuery()
                                       .FirstAsync(token);

            return result;
        }
    }
}
