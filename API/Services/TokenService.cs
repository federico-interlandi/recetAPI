using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;
public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(User user)
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokenKey from appsettings");
        if (tokenKey.Length < 64) throw new Exception("Your token key has invalid format");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.Email)
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GetTokenExpiration(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        var expirationClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp");
        if (expirationClaim != null)
        {
            var expirationUnixTimestamp = long.Parse(expirationClaim.Value);
            var expirationDate = DateTimeOffset.FromUnixTimeSeconds(expirationUnixTimestamp).UtcDateTime;
            var timeRemaining = expirationDate - DateTime.UtcNow;
            return ((int)timeRemaining.TotalSeconds).ToString();
        }

        throw new Exception("No expiration found in token.");
    }
}