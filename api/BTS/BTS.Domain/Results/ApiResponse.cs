using BTS.Domain.Models.Enums;

namespace BTS.Domain.Results
{
    public class ApiResponse<TData>
    {
        private ApiResponse(TData data)
        {
            IsSuccess = true;
            Data = data;
            Error = null;
        }

        private ApiResponse(Error error)
        {
            IsSuccess = false;
            Data = default;
            Error = error;
        }

        public bool IsSuccess { get; }
        public TData? Data { get; }
        public Error? Error { get; }

        public static ApiResponse<TData> Success(TData data) => new(data);
        public static ApiResponse<TData> Failure(Error error) => new(error);
    }

    public sealed record Error(ErrorType Type, string Object, string Message) { }
}
