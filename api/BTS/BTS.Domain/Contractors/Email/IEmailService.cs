
namespace BTS.Core.Services
{
    public interface IEmailService
    {
        Task SendEmail(CancellationToken token);
    }
}