using BTS.Domain.Models.Enums;

namespace BTS.Core.Results
{
    public class Result
    {
        // Result without any data or error
        private Result()
        {
            IsSuccess = true;
            Data = null;
            Error = null;
        }

        // Result with data
        private Result(object data)
        {
            IsSuccess = true;
            Data = data;
            Error = null;
        }

        // Result with error
        private Result(Error error)
        {
            IsSuccess = false;
            Data = default;
            Error = error;
        }

        public bool IsSuccess { get; }
        public object? Data { get; }
        public Error? Error { get; }

        public static Result Success() => new();
        public static Result Success<TData>(TData data) where TData : notnull => new(data);
        public static Result Failure(Error error) => new(error);
    }

    public sealed record Error(ErrorType Type, string Object, object Value) { }
}
