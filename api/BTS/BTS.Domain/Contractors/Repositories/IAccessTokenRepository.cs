using BTS.Domain.Contractors.Repositories.Common;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;

namespace BTS.Domain.Contractors.Repositories
{
    public interface IAccessTokenRepository : IBaseRepository<AccessToken>
    {
        Task<bool> IsTokenUsedAsync(string accessToken, CancellationToken cancellationToken);
        Task UpdateToUsedAsync(Guid userId, string token, AccessTokenType type, string requestor, CancellationToken cancellationToken);
    }
}