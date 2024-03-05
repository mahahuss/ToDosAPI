using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetId(this ClaimsPrincipal user)
    {
        var currentUserIdFromClaims = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        
        if (!int.TryParse(currentUserIdFromClaims, out var currentUserId))
        {
            return -1;
        }

        return currentUserId;
    }

    public static List<string> GetRoles(this ClaimsPrincipal user)
    {
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        return roles;
    }

    public static string? GetUsername(this ClaimsPrincipal user)
    {
        var username = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        return username;
    }

    public static string? GetFullname(this ClaimsPrincipal user)
    {
        var username = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
        return username;
    }
}