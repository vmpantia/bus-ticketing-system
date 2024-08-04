using BTS.Domain.Contractors.Repositories.Common;
using BTS.Domain.Models.Entities;

namespace BTS.Domain.Contractors.Repositories
{
    public interface IAccessTokenRepository : IBaseRepository<AccessToken>
    {
        Task<bool> IsTokenUsedAsync(string accessToken, CancellationToken cancellationToken);
    }
}