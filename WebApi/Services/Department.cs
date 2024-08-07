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

public interface IDepartmentService
{
    Task AddRange(DepartmentAddRangeDto entity);
    Task Update(int id, DepartmentUpdateDto entity);

    Task Delete(int id);

    Task<ICollection<DepartmentShortGetDto>> GetAll(int skip = 0, int take = 10);
    Task<DepartmentGetDto> GetById(int id);
    Task<DepartmentEmployeesListDto> GetByIdEmployees(
        int id, int skip = 0, int take = 10);
}

public class DepartmentService(ApplicationDbContext context) : IDepartmentService
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddRange(DepartmentAddRangeDto dtoEntity)
    {
        try
        {
            await _context.Department.AddRangeAsync(dtoEntity.Clone());
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            DbExceptionHandler.ThrowDetailedException(ex);
        }
    }

    public async Task Delete(int id)
    {
        int affectedRows = await _context.Department
            .Where(entity => entity.Id == id)
            .ExecuteDeleteAsync();

        if (affectedRows == 0)
        {
            throw new ServiceException("EntityNotFound");
        }
    }

    public async Task<ICollection<DepartmentShortGetDto>> GetAll(
        int skip = 0, int take = 10)
    {
        try
        {
            return await _context.Department
                .Where(entity => entity.IsActive)
                .Select(entity => new DepartmentShortGetDto
                {
                    Id = entity.Id,
                    Name = entity.Name
                })
                .OrderBy(entity => entity.Name)
                .Skip(skip).Take(take)
                .ToListAsync();
        }
        catch (ArgumentNullException ex)
        {
            throw new Exception("NoEntitiesFound", ex);
        }
    }

    public async Task<DepartmentGetDto> GetById(int id)
    {
        try
        {
            return await _context.Department
                .Where(entity => entity.Id == id)
                .Select(entity => new DepartmentGetDto
                {
                    Id = entity.Id,
                    Name = entity.Name,

                    IsActive = entity.IsActive,
                    CreatedOn = entity.CreatedOn,
                    UpdatedOn = entity.UpdatedOn
                })
                .FirstAsync();
        }
        catch (ArgumentNullException ex)
        {
            throw new ServiceException("EntityNotFound", ex);
        }
    }

    // Returns: List of Members of a Department Paginated
    public async Task<DepartmentEmployeesListDto> GetByIdEmployees(
        int id, int skip = 0, int take = 10)
    {
        try
        {
            return await _context.Department
                .Where(entity => entity.Id == id)
                .Select(entity => new DepartmentEmployeesListDto
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

    public async Task Update(int id, DepartmentUpdateDto dtoEntity)
    {
        int affectedRows = await _context.Department
            .Where(entity => entity.Id == id)
            .ExecuteUpdateAsync(entity => entity.SetProperty(e => e.Name, dtoEntity.Name));

        if (affectedRows == 0)
        {
            throw new ServiceException("EntityNotFound");
        }
    }
}
