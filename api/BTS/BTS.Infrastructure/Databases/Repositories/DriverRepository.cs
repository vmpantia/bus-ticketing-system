using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Entities;
using BTS.Infrastructure.Databases.Contexts;
using BTS.Infrastructure.Databases.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BTS.Infrastructure.Databases.Repositories
{
    public class DriverRepository : BaseRepository<Driver>, IDriverRepository
    {
        public DriverRepository(BTSDbContext context) : base(context) { }

        public async Task<IEnumerable<Driver>> GetDriversFullInfoAsync(CancellationToken token)
        {
            // Get all driver stored in the database
            var entities = await GetAllAsync(token);
            var result = await entities.Include(tbl => tbl.Bus)
                                       .ToListAsync(token);

            return result;
        }

        public async Task<Driver> GetDriverFullInfoAsync(Expression<Func<Driver, bool>> expression, CancellationToken token)
        {
            // Get driver stored in the database
            var entities = await GetByExpressionAsync(expression, token);
            var result = await entities.Include(tbl => tbl.Bus)
                                       .FirstAsync(token);

            return result;
        }
    }
}
