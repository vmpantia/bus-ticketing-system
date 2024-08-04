using BTS.Domain.Models;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using BTS.Domain.Contractors.Email;

namespace BTS.Infrastructure.Email
{
    public class EmailProvider : IEmailProvider
    {
        private EmailSetting _setting;
        public EmailProvider(EmailSetting setting) => _setting = setting;

        public async Task SendAsync(EmailContent content, CancellationToken token)
        {
            if (content is null) throw new ArgumentNullException(nameof(EmailContent));

            // Prepare email message or content
            var email = new MimeMessage();
            email.From.AddRange(content.From.Select(email => MailboxAddress.Parse(email)));
            email.To.AddRange(content.To.Select(email => MailboxAddress.Parse(email)));
            email.Subject = content.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = content.Body };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_setting.Host, _setting.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_setting.User, _setting.Password);
                await smtp.SendAsync(email, token);
                await smtp.DisconnectAsync(true, token);
            }
        }
    }
}
