using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartDelivery.Application.Interfaces;
using SmartDelivery.Domain.Entities;

namespace SmartDelivery.Infrastructure.Auth;

public class JwTService : IJwTService
{
    private readonly IConfiguration _configuration;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly string _secretKey;

    public JwTService(IConfiguration configuration, IDateTimeProvider dateTimeProvider, IJwtTokenGenerator jwtTokenGenerator)
    {
        _configuration = configuration;
        _dateTimeProvider = dateTimeProvider;
        _jwtTokenGenerator = jwtTokenGenerator;
        _secretKey = configuration["Jwt:SecretKey"]!;
    }

    public string GenerateToken(User user)
    {
        //var token = _jwtTokenGenerator.GenerateToken(email);
        //return token;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("fullName", user.FullName),
            new Claim("preferred", user.PreferredNotification.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}