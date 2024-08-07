/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure.Security.KeyVault.Secrets;
using Microsoft.IdentityModel.Tokens;

using WebApi.Exceptions;

namespace WebApi.Services;

public interface IJwtService
{
    string[] GenerateToken(
        int id, string role = "User", bool createRefreshToken = false,
        int accessTokenExpirationHours = 6, int refreshTokenExpirationMonths = 1,
        params Claim[] additionalClaims);

    List<Claim> ValidateTokenAndGetClaims(string jwt);
}

public class JwtService : IJwtService
{
    private readonly string _secretKey;
    private readonly string? _issuer, _audience;

    public JwtService(IConfiguration configuration, SecretClient clientSecrets)
    {
        _secretKey = clientSecrets.GetSecret(
            "SecretKey"
        ).Value.Value;

        _issuer = configuration["JWT_ISSUER"];
        _audience = configuration["JWT_AUDIENCE"];

        if (string.IsNullOrEmpty(_issuer) || string.IsNullOrEmpty(_audience))
        {
            throw new ArgumentNullException("InvalidConfiguration");
        }
    }

    public string[] GenerateToken(
        int id, string role = "User", bool createRefreshToken = false,
        int accessTokenExpirationHours = 6, int refreshTokenExpirationMonths = 1,
        params Claim[] additionalClaims)
    {
        var result = new string[2];
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, id.ToString()),
            new(ClaimTypes.Role, role)
        };

        foreach (Claim claim in additionalClaims)
        {
            claims.Add(claim);
        }

        var creds = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
            SecurityAlgorithms.HmacSha256
        );

        var accessToken = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(accessTokenExpirationHours),
            signingCredentials: creds
        );

        result[0] = new JwtSecurityTokenHandler().WriteToken(accessToken);

        if (createRefreshToken)
        {
            var refreshToken = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMonths(refreshTokenExpirationMonths),
                signingCredentials: creds
            );

            result[1] = new JwtSecurityTokenHandler().WriteToken(refreshToken);
        }

        return result;
    }

    public List<Claim> ValidateTokenAndGetClaims(string jwt)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _issuer,
            ValidAudience = _audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_secretKey))
        };
        ClaimsPrincipal claimsPrincipal;

        try
        {
            claimsPrincipal = new JwtSecurityTokenHandler()
                .ValidateToken(jwt, validationParameters, out _);
        }
        catch (Exception ex)
        {
            throw new ServiceException("ValidationFailed", ex);
        }

        return claimsPrincipal.Claims.ToList();
    }
}
