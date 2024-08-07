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

public interface IDesignationService
{
    Task AddRange(DesignationAddRangeDto entity);
    Task Update(int id, DesignationUpdateDto entity);

    Task Delete(int id);

    Task<ICollection<DesignationShortGetDto>> GetAll(int skip = 0, int take = 10);
    Task<DesignationGetDto> GetById(int id);
    Task<DesignationEmployeesListDto> GetByIdEmployees(
        int id, int skip = 0, int take = 10
    );
}

public class DesignationService(ApplicationDbContext context) : IDesignationService
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddRange(DesignationAddRangeDto dtoEntity)
    {
        try
        {
            await _context.Designation.AddRangeAsync(dtoEntity.Clone());
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            DbExceptionHandler.ThrowDetailedException(ex);
        }
    }

    public async Task Delete(int id)
    {
        int affectedRows = await _context.Designation
            .Where(entity => entity.Id == id)
            .ExecuteDeleteAsync();

        if (affectedRows == 0)
        {
            throw new ServiceException("EntityNotFound");
        }
    }

    public async Task<ICollection<DesignationShortGetDto>> GetAll(
        int skip = 0, int take = 10)
    {
        try
        {
            return await _context.Designation
                .Where(entity => entity.IsActive)
                .Select(entity => new DesignationShortGetDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    EscalationLevel = entity.EscalationLevel
                })
                .OrderByDescending(entity => entity.EscalationLevel)
                .Skip(skip).Take(take)
                .ToListAsync();
        }
        catch (ArgumentNullException ex)
        {
            throw new Exception("NoEntitiesFound", ex);
        }
    }

    public async Task<DesignationGetDto> GetById(int id)
    {
        try
        {
            return await _context.Designation
                .Where(entity => entity.Id == id)
                .Select(entity => new DesignationGetDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    EscalationLevel = entity.EscalationLevel,

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

    // Returns: List of Members of a Designation Paginated
    public async Task<DesignationEmployeesListDto> GetByIdEmployees(
        int id, int skip = 0, int take = 10)
    {
        try
        {
            return await _context.Designation
                .Where(entity => entity.Id == id)
                .Select(entity => new DesignationEmployeesListDto
                {
                    Employees = entity.Employees
                        .Where(entity => entity.IsActive)
                        .Select(entity => new EmployeeShortGetDto
                        {
                            Id = entity.Id,
                            FullName = entity.FullName
                        })
                        .OrderBy(entity => entity.FullName)
                        .Skip(skip).Take(take)
                        .ToList()
                })
                .FirstAsync();
        }
        catch (InvalidOperationException ex)
        {
            throw new ServiceException("EntityNotFound", ex);
        }
    }

    public async Task Update(int id, DesignationUpdateDto dtoEntity)
    {
        int affectedRows = await _context.Designation
            .Where(entity => entity.Id == id)
            .ExecuteUpdateAsync(entity => entity
                .SetProperty(e => e.Name, dtoEntity.Name)
                .SetProperty(e => e.EscalationLevel, dtoEntity.EscalationLevel)
            );

        if (affectedRows == 0)
        {
            throw new ServiceException("EntityNotFound");
        }
    }
}
