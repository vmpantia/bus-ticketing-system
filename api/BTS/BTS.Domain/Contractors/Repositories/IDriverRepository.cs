using BTS.Domain.Models.Entities;
using System.Linq.Expressions;

namespace BTS.Domain.Contractors.Repositories
{
    public interface IDriverRepository : IBaseRepository<Driver>
    {
        Task<IEnumerable<Driver>> GetDriversFullInfoAsync(CancellationToken token);
        Task<Driver> GetDriverFullInfoAsync(Expression<Func<Driver, bool>> expression, CancellationToken token);
    }
}