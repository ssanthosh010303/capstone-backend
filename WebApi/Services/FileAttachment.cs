/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
using Microsoft.EntityFrameworkCore;

using WebApi.Contexts;
using WebApi.Models;
using WebApi.Models.DataTransferObjects;
using WebApi.Utils;

namespace WebApi.Services;

public interface IFileAttachmentService
{
    Task Add(FileAttachmentsAddDto dtoEntity);
}

public class FileAttachmentService(ApplicationDbContext context) : IFileAttachmentService
{
    private readonly ApplicationDbContext _context = context;

    public async Task Add(FileAttachmentsAddDto dtoEntity)
    {
        try
        {
            var entities = new List<FileAttachmentModel>();

            foreach (var fileAttachment in dtoEntity.FileAttachments)
            {
                entities.Add(new FileAttachmentModel
                {
                    BlobName = fileAttachment.BlobName,
                    Title = fileAttachment.Title,
                    SubmittedWithGrievanceId = dtoEntity.SubmittedWithGrievanceId
                });
            }

            await _context.FileAttachment.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            DbExceptionHandler.ThrowDetailedException(ex);
        }

    }
}
