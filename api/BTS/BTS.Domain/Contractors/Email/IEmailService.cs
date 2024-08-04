
namespace BTS.Core.Services
{
    public interface IEmailService
    {
        Task SendAsync(CancellationToken token);
    }
}