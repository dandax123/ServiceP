using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ServiceP.Auth;

public static class TokenExtension
{
    public static int GetUserIdFromToken(this HttpContext httpContext)
    {

        Claim userIdClaim = httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
        if (userIdClaim == null)
        {
            throw new Exception("impossible");
        }
        return int.Parse(userIdClaim.Value);
    }

    public static string GetUserRoleFromToken(this HttpContext httpContext)
    {
        Claim userIdClaim = httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
        if(userIdClaim == null)
        {
            throw new Exception("impossible");
        }
        return userIdClaim.Value;
    }
}
