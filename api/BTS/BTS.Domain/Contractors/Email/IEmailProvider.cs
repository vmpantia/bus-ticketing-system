using BTS.Domain.Models;

namespace BTS.Domain.Contractors.Email
{
    public interface IEmailProvider
    {
        Task SendAsync(EmailContent content, CancellationToken token);
    }
}
