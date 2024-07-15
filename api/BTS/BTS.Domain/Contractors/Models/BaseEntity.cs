﻿using System.ComponentModel.DataAnnotations;

namespace BTS.Domain.Contractors.Models
{
    public class BaseEntity<TId, TStatus>
        where TStatus : Enum
    {
        [Key]
        public required TId Id { get; set; }
        public required TStatus Status { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}