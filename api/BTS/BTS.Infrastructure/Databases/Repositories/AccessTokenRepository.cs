using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Entities;
using BTS.Infrastructure.Databases.Contexts;
using BTS.Infrastructure.Databases.Repositories.Common;

namespace BTS.Infrastructure.Databases.Repositories
{
    public class AccessTokenRepository : BaseRepository<AccessToken>, IAccessTokenRepository
    {
        public AccessTokenRepository(BTSDbContext context) : base(context) { }

        public async Task<bool> IsTokenUsedAsync(string accessToken, CancellationToken cancellationToken) =>
            await IsExistAsync(data => data.IsUsed, cancellationToken);
    }
}
