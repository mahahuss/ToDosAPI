﻿using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ToDosAPI.Extensions;

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
}