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

public interface IGrievancePriorityService
{
    Task AddRange(GrievancePriorityAddRangeDto dtoEntity);
    Task Update(int id, GrievancePriorityUpdateDto dtoEntity);

    Task Delete(int id);

    Task<ICollection<GrievancePriorityShortGetDto>> GetAll();
    Task<GrievancePriorityGetDto> GetById(int id);
    Task<GrievancePriorityGrievanceListDto> GetByIdGrievances(
        int id, int offset = 0, int limit = 10
    );
}

public class GrievancePriorityService(
    ApplicationDbContext context) : IGrievancePriorityService
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddRange(GrievancePriorityAddRangeDto entity)
    {
        try
        {
            await _context.GrievancePriority.AddRangeAsync(entity.Clone());
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            DbExceptionHandler.ThrowDetailedException(ex);
        }
    }

    public async Task Delete(int id)
    {
        int affectedRows = await _context.GrievancePriority
            .Where(entity => entity.Id == id)
            .ExecuteDeleteAsync();

        if (affectedRows == 0)
        {
            throw new ServiceException("EntityNotFound");
        }
    }

    public async Task<ICollection<GrievancePriorityShortGetDto>> GetAll()
    {
        try
        {
            return await _context.GrievancePriority
                .Where(entity => entity.IsActive)
                .Select(entity => new GrievancePriorityShortGetDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    PriorityLevel = entity.PriorityLevel
                })
                .ToListAsync();
        }
        catch (ArgumentNullException ex)
        {
            throw new Exception("NoEntitiesFound", ex);
        }
    }

    public async Task<GrievancePriorityGetDto> GetById(int id)
    {
        try
        {
            return await _context.GrievancePriority
                .Where(entity => entity.Id == id)
                .Select(entity => new GrievancePriorityGetDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    PriorityLevel = entity.PriorityLevel,

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

    // Returns: List of Grievance of a Grievance Priority Paginated
    public async Task<GrievancePriorityGrievanceListDto> GetByIdGrievances(
        int id, int offset = 0, int limit = 10)
    {
        try
        {
            return await _context.GrievancePriority
                .Where(entity => entity.Id == id)
                .Select(entity => new GrievancePriorityGrievanceListDto
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

    public async Task Update(int id, GrievancePriorityUpdateDto dtoEntity)
    {
        int affectedRows = await _context.GrievancePriority
            .Where(entity => entity.Id == id)
            .ExecuteUpdateAsync(entity => entity
                .SetProperty(e => e.Name, dtoEntity.Name)
                .SetProperty(e => e.PriorityLevel, dtoEntity.PriorityLevel)
            );

        if (affectedRows == 0)
        {
            throw new ServiceException("EntityNotFound");
        }
    }
}
