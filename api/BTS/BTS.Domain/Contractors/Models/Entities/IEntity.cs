using System.ComponentModel.DataAnnotations;

namespace BTS.Domain.Contractors.Models.Entities
{
    public interface IEntity<TId, TStatus>
    {
        TId Id { get; set; }
        TStatus Status { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        string CreatedBy { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
        string? UpdatedBy { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
        string? DeletedBy { get; set; }
    }
}
