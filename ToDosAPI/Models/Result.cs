namespace ToDosAPI.Models;

public class Result<T>
{
    private Result()
    {
    }

    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? Error { get; set; }

    public static Result<T> Successful(T data) => new() { Success = true, Data = data };


    public static Result<T> Failure(string errorResponse)
    {
        return new Result<T>
        {
            Success = false,
            Error = errorResponse,
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