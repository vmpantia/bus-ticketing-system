namespace BTS.Domain.Models
{
    public class EmailContent
    {
        public required string Subject { get; set; }
        public required IEnumerable<string> From { get; set; }
        public required IEnumerable<string> To { get; set; }
        public IEnumerable<string> Cc { get; set; } = new List<string>();
        public IEnumerable<string> Bcc { get; set; } = new List<string>();
        public required string Body { get; set; }
    }
}
