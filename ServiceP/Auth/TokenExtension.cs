using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ServiceP.Auth;

public static class TokenExtension
{
    public static string GetUserIdFromToken(this HttpContext httpContext)
    {
    
        System.Security.Claims.Claim userIdClaim = httpContext.User.Claims.Where(x => x.Type == "email").FirstOrDefault();
        return userIdClaim.Value;
    }

    public static string GetUserRoleFromToken(this HttpContext httpContext)
    {
        System.Security.Claims.Claim userIdClaim = httpContext.User.Claims.Where(x => x.Type == "role").FirstOrDefault();
        return userIdClaim.Value;
    }
}
