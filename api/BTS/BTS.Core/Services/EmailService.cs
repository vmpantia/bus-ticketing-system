using BTS.Domain.Contractors.Email;
using BTS.Domain.Models;
using BTS.Domain.Models.Entities;

namespace BTS.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailProvider _emailProvider;
        public EmailService(IEmailProvider emailProvider) => _emailProvider = emailProvider;

        public async Task SendMagicLinkEmail(AccessToken accessToken, User user, CancellationToken cancellationToken)
        {
            var content = new EmailContent
            {
                Subject = "Login to Bus Terminal System",
                From = new List<string> { "bts-no-reply@bts.com.ph" },
                To = new List<string> { user.Email },
                Body = $"Hi Mrs. {user.LastName}, Welcome to Bus Termminal System if you wish to use our system kindly click the login button. <a href='https:/localhost:3000/login?token={accessToken.Token}' target='_blank'>Login</a>"
            };
            await _emailProvider.SendAsync(content, cancellationToken);
        }

        public async Task SendResetPasswordEmail(AccessToken accessToken, User user, CancellationToken cancellationToken)
        {
            var content = new EmailContent
            {
                Subject = "Bus Terminal System Password Reset",
                From = new List<string> { "bts-no-reply@bts.com.ph" },
                To = new List<string> { user.Email },
                Body = $"Hi Mrs. {user.LastName}, You may click the reset button to redirect you on a password reset page <a href='https:/localhost:3000/login?token={accessToken.Token}' target='_blank'>Reset</a>"
            };
            await _emailProvider.SendAsync(content, cancellationToken);
        }
    }
}
