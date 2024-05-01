using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppliancePalaceWebsite;

public class JwtProvider : IJwtProvider
{
    public string GenrateAccessToken(User user)
    {
        var claims = new List<Claim>()
        {
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("secretKey")),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
          null,
          null,
          claims,
          null,
          DateTime.UtcNow.AddDays(1),
          SigningCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}