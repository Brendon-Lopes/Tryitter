using System.Security.Claims;

namespace BackEndTryitter.Services.Authorization;

public static class AuthorizationServices
{
    public static bool CheckAuthorization(HttpContext httpContext, Guid id)
    {
        var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
        if (claimsIdentity == null) return false;

        var tokenId = claimsIdentity.Claims.First(c => c.Type == "userId").Value;
        return tokenId == id.ToString();
    }
}