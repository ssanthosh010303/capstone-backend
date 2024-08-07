/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace WebApi.Authentication;
using WebApi.Exceptions;
using WebApi.Services;

public class HttpBasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IEmployeeService service)
    : AuthenticationHandler<AuthenticationSchemeOptions>(
        options, logger, encoder)
{
    private readonly IEmployeeService _service = service;

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("MissingHeader");
        }

        string? authorizationHeader = Request.Headers.Authorization;

        if (string.IsNullOrEmpty(authorizationHeader) ||
            !authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.Fail("InvalidHeader");
        }

        string[] credentials = Encoding.UTF8.GetString(
            Convert.FromBase64String(authorizationHeader.Substring(6))
        ).Split(":");

        if (credentials?.Length != 2)
        {
            return AuthenticateResult.Fail("InvalidHeader");
        }

        string username = credentials[0];
        string password = credentials[1];

        int employeeId;
        string employeeRole;

        try
        {
            (employeeId, employeeRole) = await _service.ValidateEmployee(username, password);
        }
        catch (ServiceException)
        {
            return AuthenticateResult.Fail("InvalidUsernamePassword");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, employeeId.ToString()),
            new Claim(ClaimTypes.Role, employeeRole)
        };

        var identity = new ClaimsIdentity(claims, "Basic");
        var claimsPrincipal = new ClaimsPrincipal(identity); // Create HTTP Context
        return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
    }
}
