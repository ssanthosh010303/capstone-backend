/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebApi.Exceptions;
using WebApi.Models.DataTransferObjects;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/grievance-response")]
[Authorize(Roles = "Admin,Manager,User,Microservice")]
public class GrievanceResponseController(IGrievanceResponseService service) : ControllerBase
{
    private readonly IGrievanceResponseService _service = service;

    [HttpPost]
    public async Task<IActionResult> Add(GrievanceResponseAddDto dtoEntity)
    {
        try
        {
            return Ok(await _service.Add(GetEmployeeId(), dtoEntity));
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

    [HttpGet]
    public async Task<IActionResult> GetAll(int grievanceid, int skip = 0, int take = 10)
    {
        try
        {
            return Ok(await _service.GetAll(GetEmployeeId(), grievanceid, skip, take));
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

    private int GetEmployeeId()
    {
        string? id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
        {
            throw new ServiceException("InvalidJwt");
        }

        return int.Parse(id);
    }
}
