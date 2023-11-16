using Microsoft.AspNetCore.Mvc;
using ToDosAPI.Models;

namespace ToDosAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    [NonAction]
    public OkObjectResult Ok(string? message = null)
    {
        return base.Ok(new ApiResponse(200, message ?? "Operation completed successfully"));
    }

    [NonAction]
    public BadRequestObjectResult BadRequest(string? message = null)
    {
        return base.BadRequest(new ApiResponse(400, message ?? "Something went wrong"));
    }

    [NonAction]
    public UnauthorizedObjectResult Unauthorized(string? message = null)
    {
        return base.Unauthorized(new ApiResponse(401, message ?? "You cannot access this resource"));
    }
}