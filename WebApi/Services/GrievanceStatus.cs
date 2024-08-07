/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
using Microsoft.EntityFrameworkCore;

using WebApi.Contexts;
using WebApi.Exceptions;
using WebApi.Models.DataTransferObjects;
using WebApi.Utils;

namespace WebApi.Services;

public interface IGrievanceStatusService
{
    Task AddRange(GrievanceStatusAddRangeDto dtoEntity);
    Task Update(int id, GrievanceStatusUpdateDto dtoEntity);

    Task Delete(int id);

    Task<ICollection<GrievanceStatusShortGetDto>> GetAll();
    Task<GrievanceStatusGetDto> GetById(int id);
    Task<GrievanceStatusGrievanceListDto> GetByIdGrievances(
        int id, int offset = 0, int limit = 10
    );
}

public class GrievanceStatusService(
    ApplicationDbContext context) : IGrievanceStatusService
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddRange(GrievanceStatusAddRangeDto dtoEntity)
    {
        try
        {
            await _context.GrievanceStatus.AddRangeAsync(dtoEntity.Clone());
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            DbExceptionHandler.ThrowDetailedException(ex);
        }
    }

    public async Task Delete(int id)
    {
        int affectedRows = await _context.GrievanceStatus
            .Where(entity => entity.Id == id)
            .ExecuteDeleteAsync();

        if (affectedRows == 0)
        {
            throw new ServiceException("EntityNotFound");
        }
    }

    // XXX: Pagination Removed (unncecessary)
    public async Task<ICollection<GrievanceStatusShortGetDto>> GetAll()
    {
        try
        {
            return await _context.GrievanceStatus
                .Where(entity => entity.IsActive)
                .Select(entity => new GrievanceStatusShortGetDto
                {
                    Id = entity.Id,
                    Name = entity.Name
                })
                .OrderBy(entity => entity.Name)
                .ToListAsync();
        }
        catch (ArgumentNullException ex)
        {
            throw new Exception("NoEntitiesFound", ex);
        }
    }

    public async Task<GrievanceStatusGetDto> GetById(int id)
    {
        try
        {
            return await _context.GrievanceStatus
                .Where(entity => entity.Id == id)
                .Select(entity => new GrievanceStatusGetDto
                {
                    Id = entity.Id,
                    Name = entity.Name,

                    IsActive = entity.IsActive,
                    CreatedOn = entity.CreatedOn,
                    UpdatedOn = entity.UpdatedOn
                })
                .FirstAsync();
        }
        catch (InvalidOperationException ex)
        {
            throw new ServiceException("EntityNotFound", ex);
        }
    }

    // Returns: List of Grievance of a Grievance Status Paginated
    public async Task<GrievanceStatusGrievanceListDto> GetByIdGrievances(
        int id, int offset = 0, int limit = 10)
    {
        try
        {
            return await _context.GrievanceStatus
                .Where(entity => entity.Id == id)
                .Select(entity => new GrievanceStatusGrievanceListDto
                {
                    Grievances = entity.Grievances
                        .Where(entity => entity.IsActive)
                        .Select(entity => new GrievanceShortGetDto
                        {
                            Id = entity.Id,
                            Title = entity.Title,
                            Deadline = entity.Deadline
                        })
                        .OrderByDescending(entity => entity.Deadline)
                        .Skip(offset).Take(limit)
                        .ToList()
                })
                .FirstAsync();
        }
        catch (InvalidOperationException ex)
        {
            throw new ServiceException("EntityNotFound", ex);
        }
    }

    public async Task Update(int id, GrievanceStatusUpdateDto dtoEntity)
    {
        int affectedRows = await _context.GrievanceStatus
            .Where(entity => entity.Id == id)
            .ExecuteUpdateAsync(entity => entity
                .SetProperty(e => e.Name, dtoEntity.Name)
            );

        if (affectedRows == 0)
        {
            throw new ServiceException("EntityNotFound");
        }
    }
}
