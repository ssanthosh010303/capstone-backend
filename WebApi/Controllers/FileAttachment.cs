/*
 * Author: Apache X692 Attack Helicopter
 * Created: 24/07/2024
 */
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebApi.Exceptions;
using WebApi.Models.DataTransferObjects;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/file-attachment")]
[Authorize(Roles = "Admin,Manager,User")]
public class FileAttachment(
        IAzureBlobService blobService,
        IFileAttachmentService fileAttachmentService)
    : ControllerBase
{
    private readonly IAzureBlobService _blobService = blobService;
    private readonly IFileAttachmentService _fileAttachmentService = fileAttachmentService;

    [HttpPost("upload-urls")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult GetUploadUrls(AzureBlobGetUploadUrlsDto dtoEntity)
    {
        try
        {
            return Ok(_blobService.GetUploadUrls(dtoEntity));
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

    [HttpPost("download-url")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult GetDownloadUrl(string blobname)
    {
        try
        {
            return Ok(_blobService.GetDownloadUrl(blobname));
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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(FileAttachmentsAddDto dtoEntity)
    {
        try
        {
            await _fileAttachmentService.Add(dtoEntity);
            return Created();
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
}
