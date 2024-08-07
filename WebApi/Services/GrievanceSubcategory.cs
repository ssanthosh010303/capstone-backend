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

public interface IGrievanceSubcategoryService
{
    Task AddRange(GrievanceSubcategoryAddRangeDto dtoEntity);
    Task Update(int id, GrievanceSubcategoryUpdateDto dtoEntity);

    Task Delete(int id);

    Task<ICollection<GrievanceSubcategoryShortGetDto>> GetAll(int categoryId);
    Task<GrievanceSubcategoryGetDto> GetById(int id);
    Task<GrievanceSubcategoryGrievanceListDto> GetByIdGrievances(
        int id, int skip = 0, int take = 10
    );
}

public class GrievanceSubcategoryService(
        ApplicationDbContext context) : IGrievanceSubcategoryService
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddRange(GrievanceSubcategoryAddRangeDto dtoEntity)
    {
        try
        {
            await _context.GrievanceSubcategory.AddRangeAsync(dtoEntity.Clone());
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            DbExceptionHandler.ThrowDetailedException(ex);
        }
    }

    public async Task Delete(int id)
    {
        int affectedRows = await _context.GrievanceSubcategory
            .Where(entity => entity.Id == id)
            .ExecuteDeleteAsync();

        if (affectedRows == 0)
        {
            throw new ServiceException("EntityNotFound");
        }
    }

    public async Task<ICollection<GrievanceSubcategoryShortGetDto>> GetAll(
        int categoryId)
    {
        try
        {
            return await _context.GrievanceSubcategory
                .Where(entity => entity.IsActive && entity.GrievanceCategoryId == categoryId)
                .Select(entity => new GrievanceSubcategoryShortGetDto
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

    public async Task<GrievanceSubcategoryGetDto> GetById(int id)
    {
        try
        {
            return await _context.GrievanceSubcategory
                .Select(entity => new GrievanceSubcategoryGetDto
                {
                    Id = entity.Id,
                    Name = entity.Name,

                    GrievanceCategory = new GrievanceCategoryShortGetDto
                    {
                        Id = entity.GrievanceCategory.Id,
                        Name = entity.GrievanceCategory.Name
                    },

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

    // Returns: List of Grievance of a Grievance Subcategory Paginated
    public async Task<GrievanceSubcategoryGrievanceListDto> GetByIdGrievances(
        int id, int skip = 0, int take = 10)
    {
        try
        {
            return await _context.GrievanceSubcategory
                .Where(entity => entity.Id == id)
                .Select(entity => new GrievanceSubcategoryGrievanceListDto
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

    public async Task Update(int id, GrievanceSubcategoryUpdateDto dtoEntity)
    {
        int affectedRows = await _context.GrievanceSubcategory
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
