using BTS.Domain.Models.Entities;

namespace BTS.Domain.Contractors.Repositories
{
    public interface IDriverRepository
    {
        Task<IEnumerable<Driver>> GetDriversFullInfoAsync(CancellationToken token);
    }
}