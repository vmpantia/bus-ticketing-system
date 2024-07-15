using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;
using BTS.Infrastructure.Databases.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BTS.Infrastructure.Databases.Repositories
{
    public class DriverRepository : BaseRepository<Driver>, IDriverRepository
    {
        public DriverRepository(BTSDbContext context) : base(context) { }

        public async Task<IEnumerable<Driver>> GetDriversFullInfoAsync(CancellationToken token)
        {
            // Get drivers that is not deleted
            var entities = await GetByExpressionAsync(data => data.Status != CommonStatus.Deleted, token);
            var result = await entities.Include(tbl => tbl.Bus)
                                       .ToListAsync();

            return result;
        }
    }
}
