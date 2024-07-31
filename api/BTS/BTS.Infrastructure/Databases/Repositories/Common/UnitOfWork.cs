using BTS.Domain.Contractors.Repositories.Common;
using BTS.Infrastructure.Databases.Contexts;

namespace BTS.Infrastructure.Databases.Repositories.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BTSDbContext _context;
        public UnitOfWork(BTSDbContext context) => _context = context;

        public async Task SaveChangesAsync(CancellationToken token) => await _context.SaveChangesAsync(token);
    }
}
