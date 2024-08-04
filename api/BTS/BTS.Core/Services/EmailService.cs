using BTS.Domain.Contractors.Email;

namespace BTS.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailProvider _emailProvider;
        public EmailService(IEmailProvider emailProvider) => _emailProvider = emailProvider;

        public async Task SendEmail(CancellationToken token)
        {
            await _emailProvider.SendAsync(new Domain.Models.EmailContent
            {
                Subject = "Test",
                From = new List<string> { "vincent.m.pantia@gmail.com" },
                To = new List<string> { "vincent.m.pantia@gmail.com" },
                Body = "Test Email"
            }, token);
        }
    }
}
