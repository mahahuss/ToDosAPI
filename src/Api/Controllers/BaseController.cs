using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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
    [NonAction]
    public NotFoundObjectResult NotFound(string? message = null)
    {
        return base.NotFound(new ApiResponse(404, message ?? "This resource not found"));
    }
}