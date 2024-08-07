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

public interface IGrievanceCategoryService
{
    Task AddRange(GrievanceCategoryAddRangeDto dtoEntity);
    Task Update(int id, GrievanceCategoryUpdateDto dtoEntity);

    Task Delete(int id);

    Task<ICollection<GrievanceCategoryShortGetDto>> GetAll();
    Task<GrievanceCategoryGetDto> GetById(int id);
    Task<GrievanceCategoryGrievanceSubcategoryListDto> GetByIdGrievanceSubcategory(
        int id, int skip = 0, int take = 10
    );
}

public class GrievanceCategoryService(
    ApplicationDbContext context) : IGrievanceCategoryService
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddRange(GrievanceCategoryAddRangeDto dtoEntity)
    {
        try
        {
            await _context.GrievanceCategory.AddRangeAsync(dtoEntity.Clone());
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            DbExceptionHandler.ThrowDetailedException(ex);
        }
    }

    public async Task Delete(int id)
    {
        int affectedRows = await _context.GrievanceCategory
            .Where(entity => entity.Id == id)
            .ExecuteDeleteAsync();

        if (affectedRows == 0)
        {
            throw new ServiceException("EntityNotFound");
        }
    }

    public async Task<ICollection<GrievanceCategoryShortGetDto>> GetAll()
    {
        try
        {
            return await _context.GrievanceCategory
                .Where(entity => entity.IsActive)
                .Select(entity => new GrievanceCategoryShortGetDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description
                })
                .OrderBy(entity => entity.Name)
                .ToListAsync();
        }
        catch (ArgumentNullException ex)
        {
            throw new Exception("NoEntitiesFound", ex);
        }
    }

    public async Task<GrievanceCategoryGetDto> GetById(int id)
    {
        try
        {
            return await _context.GrievanceCategory
                .Where(entity => entity.Id == id)
                .Select(entity => new GrievanceCategoryGetDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,

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

    // Returns: List of Members of a GrievanceCategory Paginated
    public async Task<GrievanceCategoryGrievanceSubcategoryListDto> GetByIdGrievanceSubcategory(
        int id, int skip = 0, int take = 10)
    {
        try
        {
            return await _context.GrievanceCategory
                .Where(entity => entity.Id == id)
                .Select(entity => new GrievanceCategoryGrievanceSubcategoryListDto
                {
                    GrievanceSubcategories = entity.GrievanceSubcategories
                        .Where(entity => entity.IsActive)
                        .Select(entity => new GrievanceSubcategoryShortGetDto
                        {
                            Id = entity.Id,
                            Name = entity.Name
                        })
                        .OrderBy(entity => entity.Name)
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

    public async Task Update(int id, GrievanceCategoryUpdateDto dtoEntity)
    {
        int affectedRows = await _context.GrievanceCategory
            .Where(entity => entity.Id == id)
            .ExecuteUpdateAsync(entity => entity
                .SetProperty(e => e.Name, dtoEntity.Name)
                .SetProperty(e => e.Description, dtoEntity.Description)
            );

        if (affectedRows == 0)
        {
            throw new ServiceException("EntityNotFound");
        }
    }
}
