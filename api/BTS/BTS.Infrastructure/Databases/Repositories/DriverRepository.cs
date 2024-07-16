using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Entities;
using BTS.Infrastructure.Databases.Contexts;
using Microsoft.EntityFrameworkCore;

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
    }
}
