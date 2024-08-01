using BTS.Domain.Contractors.Repositories.Common;
using BTS.Domain.Models.Entities;
using System.Linq.Expressions;

namespace BTS.Domain.Contractors.Repositories
{
    public interface IBusRepository : IBaseRepository<Bus>
    {
        Task<IEnumerable<Bus>> GetBusesFullInfoAsync(CancellationToken token);
        Task<Bus> GetBusFullInfoAsync(Expression<Func<Bus, bool>> expression, CancellationToken token);
    }
}