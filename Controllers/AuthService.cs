using Microsoft.Extensions.Caching.Memory;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EcommerceTrail.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class AuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IMemoryCache _cache;


    public AuthService(IOptions<JwtSettings> jwtOptions, IMemoryCache cache)
    {
        _jwtSettings = jwtOptions.Value;
        _cache = cache;
    }

    public string GenerateJwtToken(string email, string typeofuser)
    {
        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Email, email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Role, typeofuser) // Add role claim
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.LifespanMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiration,
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        _cache.Set(email, tokenString, expiration);

        return tokenString;
    }


    public bool IsTokenValid(string email, string token)
    {
        return _cache.TryGetValue(email, out string cachedToken) && cachedToken == token;
    }

    public void ExpireToken(string email)
    {

        _cache.Remove(email);
    }
}


