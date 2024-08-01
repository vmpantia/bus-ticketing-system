using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Entities;
using BTS.Infrastructure.Databases.Contexts;
using BTS.Infrastructure.Databases.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BTS.Infrastructure.Databases.Repositories
{
    public class BusRepository : BaseRepository<Bus>, IBusRepository
    {
        public BusRepository(BTSDbContext context) : base(context) { }

        public async Task<IEnumerable<Bus>> GetBusesFullInfoAsync(CancellationToken token)
        {
            // Get all bus stored in the database
            var entities = await GetAllAsync(token);
            var result = await entities.Include(tbl => tbl.Driver)
                                       .Include(tbl => tbl.Route)
                                       .AsSplitQuery()
                                       .ToListAsync(token);

            return result;
        }

        public async Task<Bus> GetBusFullInfoAsync(Expression<Func<Bus, bool>> expression, CancellationToken token)
        {
            // Get bus stored in the database
            var entities = await GetByExpressionAsync(expression, token);
            var result = await entities.Include(tbl => tbl.Driver)
                                       .Include(tbl => tbl.Route)
                                       .AsSplitQuery()
                                       .FirstAsync(token);

            return result;
        }
    }
}
