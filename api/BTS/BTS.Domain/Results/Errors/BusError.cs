﻿using BTS.Domain.Constants;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;

namespace BTS.Domain.Results.Errors
{
    public class BusError
    {
        public static Error Null => new(ErrorType.NULL, nameof(Bus), "Bus(s) result cannot be NULL.");
        public static Error NotFound => new(ErrorType.NotFound, nameof(Bus), string.Format(ErrorMessage.ERROR_NOT_FOUND_FORMAT, nameof(Bus)));
    }
}
