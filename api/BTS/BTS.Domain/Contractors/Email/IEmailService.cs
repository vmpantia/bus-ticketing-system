
using BTS.Domain.Models.Entities;

namespace BTS.Core.Services
{
    public interface IEmailService
    {
        Task SendMagicLinkEmail(AccessToken accessToken, User user, CancellationToken cancellationToken);
    }
}