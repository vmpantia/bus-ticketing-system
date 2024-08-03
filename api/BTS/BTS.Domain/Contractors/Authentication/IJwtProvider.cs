﻿using BTS.Domain.Models.Entities;

namespace BTS.Domain.Contractors.Authentication
{
    public interface IJwtProvider
    {
        string Generate(User user);
        string? GetValueByClaim(string claimName, string token);
    }
}