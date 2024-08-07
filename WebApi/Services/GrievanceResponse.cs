/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
using Microsoft.EntityFrameworkCore;

using WebApi.Contexts;
using WebApi.Exceptions;
using WebApi.Models;
using WebApi.Models.DataTransferObjects;
using WebApi.Utils;

namespace WebApi.Services;

public interface IGrievanceResponseService
{
    Task<GrievanceResponseShortGetDto> Add(
        int employeeId, GrievanceResponseAddDto dtoEntity
    );

    Task<ICollection<GrievanceResponseShortGetDto>> GetAll(
        int employeeId, int grievanceId, int skip, int take
    );
}

public class GrievanceResponseService(
    ApplicationDbContext context) : IGrievanceResponseService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<GrievanceResponseShortGetDto> Add(int employeeId, GrievanceResponseAddDto dtoEntity)
    {
        // Throws: Unauthorized
        int creatorId = await IsEmployeeAuthorizedToCreateResponse(
            employeeId, dtoEntity.GrievanceId
        );

        var newEntity = dtoEntity.Clone(new());

        // Set Defaults
        newEntity.CreatedById = creatorId;
        newEntity.GrievanceId = dtoEntity.GrievanceId;
        newEntity.MessageType = GrievanceResponseMessageType.Message;

        // TODO: Call Scheduler Microservice to Assign Grievance

        try
        {
            await _context.Set<GrievanceResponseModel>().AddAsync(newEntity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            DbExceptionHandler.ThrowDetailedException(ex);
        }

        var grievanceResponse = await _context.GrievanceResponse
            .Where(entity => entity.Id == newEntity.Id)
            .Select(entity => new GrievanceResponseShortGetDto
            {
                Id = entity.Id,
                Description = entity.Description,
                MessageType = entity.MessageType,
                CreatedById = entity.CreatedById,
                CreatedOn = entity.CreatedOn
            })
            .FirstAsync();

        return grievanceResponse;
    }

    public async Task<ICollection<GrievanceResponseShortGetDto>> GetAll(
        int employeeId, int grievanceId, int skip = 0, int take = 10)
    {
        // Throws: Unauthorized
        _ = await IsEmployeeAuthorizedToCreateResponse(employeeId, grievanceId);

        try
        {
            return await _context.GrievanceResponse
                .Where(entity => entity.GrievanceId == grievanceId)
                .Select(entity => new GrievanceResponseShortGetDto
                {
                    Id = entity.Id,
                    CreatedById = entity.CreatedById,
                    Description = entity.Description,
                    MessageType = entity.MessageType,
                    CreatedOn = entity.CreatedOn
                })
                .OrderBy(entity => entity.CreatedOn)
                .Skip(skip).Take(take)
                .ToListAsync();
        }
        catch (ArgumentNullException ex)
        {
            throw new ServiceException("GrievanceNotFound", ex);
        }
    }

    // Utility
    private async Task<int> IsEmployeeAuthorizedToCreateResponse(
        int employeeId, int grievanceId)
    {
        try
        {
            var ids = await _context.Grievance
                .Where(entity => entity.Id == grievanceId)
                .Select(entity => new { entity.CreatedById, entity.AssignedToId })
                .FirstAsync();

            if (ids.CreatedById != employeeId && ids.AssignedToId != employeeId)
            {
                throw new ServiceException("Unauthorized");
            }

            return (ids.CreatedById == employeeId) ? ids.CreatedById : ids.AssignedToId ?? 2;
        }
        catch (InvalidOperationException ex)
        {
            throw new ServiceException("GrievanceNotFound", ex);
        }
    }
}
