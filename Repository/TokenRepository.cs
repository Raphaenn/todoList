using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TodoList.Repository.Interfaces;

namespace TodoList.Repository;

public class TokenRepository : ITokenRepository
{
    private readonly IConfiguration _iconfiguration;
    public TokenRepository(IConfiguration configuration)
    {
        _iconfiguration = configuration;
    }
    
    public string CreateJwtToken(IdentityUser user, List<string> roles)
    {
        // create claims
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Email, user.Email));
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfiguration["Jwt:Key"]));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _iconfiguration["Jwt:Issuer"],
            _iconfiguration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}