using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Extensions;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;
using BTS.Infrastructure.Databases.Contexts;
using BTS.Infrastructure.Databases.Repositories.Common;

namespace BTS.Infrastructure.Databases.Repositories
{
    public class AccessTokenRepository : BaseRepository<AccessToken>, IAccessTokenRepository
    {
        public AccessTokenRepository(BTSDbContext context) : base(context) { }

        public async Task<bool> IsTokenUsedAsync(string accessToken, CancellationToken cancellationToken) =>
            await IsExistAsync(data => data.IsUsed, cancellationToken);

        public async Task UpdateToUsedAsync(Guid userId, string token, AccessTokenType type, string requestor, CancellationToken cancellationToken)
        {
            // Get access token based on the parameters provided
            var accessToken = GetOne(data => data.UserId == userId &&
                                             data.Token.Equals(token) &&
                                             data.Type == type &&
                                             data.IsUsed == false);

            // Update necessary information
            accessToken.IsUsed = true;
            accessToken.UsedAt = DateTimeExtension.GetCurrentDateTimeOffsetUtc();
            accessToken.UsedBy = requestor;
            accessToken.UpdatedAt = DateTimeExtension.GetCurrentDateTimeOffsetUtc();
            accessToken.UpdatedBy = requestor;
            await UpdateAsync(accessToken, cancellationToken);
        }
    }
}
