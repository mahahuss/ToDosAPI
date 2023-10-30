using System.Net;

namespace ToDosAPI;


public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var errorId = Guid.NewGuid().ToString("N");

            _logger.LogError(ex, "Something went wrong, {ErrorId}", errorId);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (_env.IsDevelopment())
            {
                await context.Response.WriteAsJsonAsync(new
                {
                    StatusCode = 500,
                    ex.Message
                });
                return;
            }


            await context.Response.WriteAsJsonAsync(new
            {
                StatusCode = 500,
                Message = $"Something went wrong, please contact support: {errorId}"
            });
        }
    }
}
