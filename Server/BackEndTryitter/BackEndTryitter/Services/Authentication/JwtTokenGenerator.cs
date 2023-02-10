using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BackEndTryitter.Services.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    public string GenerateToken(Guid userId, string fullName, string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = AddClaims(userId, fullName, username),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Supersafesecret123!")),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Expires = DateTime.Now.AddDays(1),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private ClaimsIdentity AddClaims(Guid userId, string fullName, string username)
    {
        var claims = new ClaimsIdentity();

        claims.AddClaim(new Claim("userId", userId.ToString()));
        claims.AddClaim(new Claim("fullName", fullName));
        claims.AddClaim(new Claim("username", username));

        return claims;
    }
}