using AuthApi.Models;
using AuthApi.Services.Interfaces;
using SharedApi;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly List<User> _users = new List<User>()
    {
        new("admin", "admin", "Administrator", new[] { "shoes.read" }),
        new("user01", "user01", "User", new[] { "shoes.read" })
    };

    public AuthenticationToken? GenerateAuthToken(LoginModel loginModel)
    {
        User? user = _users.FirstOrDefault(u => u.Username == loginModel.Username && u.Password == loginModel.Password);

        if (user is null)
            return null;

        SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.TokenSecurityKey));
        SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        DateTime expirationTimeStamp = DateTime.Now.AddMinutes(5);

        List<Claim> claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Username),
            new Claim("role", user.Role),
            new Claim("scope", string.Join(" ", user.Scopes))
        };

        JwtSecurityToken tokenOptions = new JwtSecurityToken(
            issuer: "https://localhost:5002",
            audience: "https://localhost:5002",
            claims: claims,
            expires: expirationTimeStamp,
            signingCredentials: signingCredentials
        );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return new AuthenticationToken(tokenString, (int)expirationTimeStamp.Subtract(DateTime.Now).TotalSeconds);
    }
}