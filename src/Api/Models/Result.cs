
namespace Api.Models;

public enum ResultErrorType
{
    Unauthorized,
    BadRequest,
    NotFound,
    Unknown
}

public class Result<T>
{
    private Result()
    {
    }

    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? Error { get; set; }
    public ResultErrorType? ErrorType { get; set; }

    public static Result<T> Successful(T data) => new() { Success = true, Data = data };


    public static Result<T> Failure(string errorResponse, ResultErrorType errorType = ResultErrorType.BadRequest)
    {
        return new Result<T>
        {
            Success = false,
            Error = errorResponse,
            ErrorType = errorType
        };
    }

    public static implicit operator Result<T>(T value)
    {
        return Successful(value);
    }

    public TResult Match<TResult>(Func<T?, TResult> success, Func<string?, TResult> failure)
    {
        return Success ? success(Data) : failure(Error);
    }
}