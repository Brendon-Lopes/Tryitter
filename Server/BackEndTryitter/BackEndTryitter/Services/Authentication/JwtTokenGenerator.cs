using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackEndTryitter.Models;
using Microsoft.IdentityModel.Tokens;

namespace BackEndTryitter.Services.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = AddClaims(user),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Supersafesecret123!")),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Expires = DateTime.UtcNow.AddDays(1),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private ClaimsIdentity AddClaims(User user)
    {
        var claims = new ClaimsIdentity();

        claims.AddClaim(new Claim("userId", user.UserId.ToString()));
        claims.AddClaim(new Claim("fullName", user.FullName));
        claims.AddClaim(new Claim("username", user.Username));

        return claims;
    }
}