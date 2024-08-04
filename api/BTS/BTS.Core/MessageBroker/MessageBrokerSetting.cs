using Microsoft.Extensions.Configuration;

namespace BTS.Core.MessageBroker
{
    public class MessageBrokerSetting
    {
        public const string SectionName = "MessageBroker";
        private MessageBrokerSetting(string host, string user, string password)
        {
            Host = host;
            User = user;
            Password = password;
        }

        public string Host { get; init; }
        public string User { get; init; }
        public string Password { get; init; }

        public static MessageBrokerSetting FromConfiguration(IConfiguration configuration)
        {
            var host = configuration[$"{SectionName}:{nameof(Host)}"]!;
            var user = configuration[$"{SectionName}:{nameof(User)}"]!;
            var password = configuration[$"{SectionName}:{nameof(Password)}"]!;

            return new(host, user, password);
        }
    }
}
