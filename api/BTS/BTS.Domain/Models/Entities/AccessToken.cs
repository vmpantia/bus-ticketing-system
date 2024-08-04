using BTS.Domain.Models.Enums;

namespace BTS.Domain.Models.Entities
{
    public class AccessToken
    {
        public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required string Token { get; set; }
        public required AccessTokenType Type { get; set; }
        public bool IsUsed { get; set; } = false;
        public DateTimeOffset? UsedAt { get; set; }
        public string? UsedBy { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
        public required string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
