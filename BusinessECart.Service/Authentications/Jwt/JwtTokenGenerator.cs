using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessECart.Service.Authentications.Jwt;

public class JwtTokenGenerator(IConfiguration configuration) : IJwtTokenGenerator
{
    public string GenerateToken(IdentityUser user)
    {
        var key = configuration["Jwt:Key"];
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        var expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration["Jwt:ExpiresInMinutes"] ?? "60"));

        // امنیت: بررسی عدم null بودن کلید
        ArgumentNullException.ThrowIfNull(key);

        var keyBytes = Encoding.UTF8.GetBytes(key);
        if (keyBytes.Length < 32)
            throw new ArgumentException("JWT key must be at least 32 bytes for HS256.");

        var securityKey = new SymmetricSecurityKey(keyBytes);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}