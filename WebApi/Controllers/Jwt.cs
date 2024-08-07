/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebApi.Exceptions;
using WebApi.Models.DataTransferObjects;
using WebApi.Services;

namespace WebApi.Controllers;

[Route("/api/jwt")]
[ApiController]
public class JwtController(IJwtService service) : ControllerBase
{
    private readonly IJwtService _service = service;

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Basic", Roles = "Admin,Manager,User")]
    [ProducesResponseType(typeof(JwtGetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult GenerateToken()
    {
        try
        {
            (int id, string role) = GetClaims();

            var tokens = _service.GenerateToken(id, role, true);

            return Ok(new JwtGetDto
            {
                AccessToken = tokens[0],
                RefreshToken = tokens[1]
            });
        }
        catch (ServiceException ex)
        {
            return BadRequest(new ProblemDetails
            {
                Title = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Title = "InternalServerError",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }

    [HttpGet("refresh")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Manager,User")]
    [ProducesResponseType(typeof(JwtRefreshDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult RefreshToken()
    {
        try
        {
            (int id, string role) = GetClaims();

            return Ok(new JwtRefreshDto
            {
                AccessToken = _service.GenerateToken(id, role)[0]
            });
        }
        catch (ServiceException ex)
        {
            return BadRequest(new ProblemDetails
            {
                Title = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Title = "InternalServerError",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }

    private (int, string) GetClaims()
    {
        int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "-1");
        string? role = User.FindFirstValue(ClaimTypes.Role);

        if (id == -1 || role == null)
        {
            throw new ServiceException("InvalidJwt");
        }

        return (id, role);
    }
}
