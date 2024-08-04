using Microsoft.Extensions.Configuration;

namespace BTS.Infrastructure.Email
{
    public class EmailSetting
    {
        private const string SectionName = "Smtp";
        private EmailSetting(string host, int port, string user, string password)
        {
            Host = host;
            Port = port;
            User = user;
            Password = password;
        }

        public string Host { get; init; }
        public int Port { get; init; }
        public string User { get; init; }
        public string Password { get; init; }

        public static EmailSetting FromConfiguration(IConfiguration configuration)
        {
            var host = configuration[$"{SectionName}:{nameof(Host)}"]!;
            var port = int.Parse(configuration[$"{SectionName}:{nameof(Port)}"]!);
            var user = configuration[$"{SectionName}:{nameof(User)}"]!;
            var password = configuration[$"{SectionName}:{nameof(Password)}"]!;

            return new(host, port, user, password);
        }
    }
}
