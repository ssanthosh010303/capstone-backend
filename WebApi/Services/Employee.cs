/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
using Microsoft.EntityFrameworkCore;

using WebApi.Exceptions;
using WebApi.Models.DataTransferObjects;
using System.Security.Claims;
using WebApi.Contexts;
using WebApi.Utils;
using WebApi.Email;

namespace WebApi.Services;

public interface IEmployeeService
{
    Task Add(EmployeeAddDto dtoEntity);
    void Update(int id, EmployeeUpdateDto dtoEntity);

    Task<ICollection<EmployeeShortGetDto>> GetAll(int skip, int take);
    Task<EmployeeGetDto> GetById(int id);

    Task UpdateEmail(EmployeeEmailUpdateDto dtoEntity);
    Task FinishEmailUpdate(string jwt);

    Task<(int, string)> ValidateEmployee(string username, string password);

    Task ForgotPassword(EmployeeForgotPasswowrdDto dtoEntity);
    Task ResetPassword(string jwt, EmployeeResetPasswordDto dtoEntity);
    Task SetPassword(int id, EmployeeSetPasswordDto entity);

    Task Delete(int id);
}

public class EmployeeService(
    ApplicationDbContext context,
    ICryptographyService cryptographyService,
    IJwtService jwtService,
    IEmailService emailService,
    IEmailTemplateLoader emailTemplateLoader
) : IEmployeeService
{
    private readonly ApplicationDbContext _context = context;
    private readonly ICryptographyService _cryptographyService = cryptographyService;
    private readonly IJwtService _jwtService = jwtService;

    private readonly IEmailService _emailService = emailService;
    private readonly IEmailTemplateLoader _emailTemplateLoader = emailTemplateLoader;

    public async Task Add(EmployeeAddDto entity)
    {
        try
        {
            var newEmployee = entity.Clone(new());

            newEmployee.PasswordHash = _cryptographyService.HashPassword(
                newEmployee.PasswordHash
            );

            newEmployee.RoleId = 3; // XXX: Hardcoded (from seeders)

            await _context.Employee.AddAsync(newEmployee);
            await _context.SaveChangesAsync();

            // Email
            (string subject, string body) = _emailTemplateLoader.GetTemplate(
                EmailTemplateTypes.Welcome, newEmployee.FullName
            );

            await _emailService.SendEmailAsync(
                [newEmployee.Email], subject, body
            );
        }
        catch (DbUpdateException ex)
        {
            DbExceptionHandler.ThrowDetailedException(ex);
        }
    }

    public async Task Delete(int id)
    {
        int affectedRows = await _context.Employee
            .Where(entity => entity.Id == id)
            .ExecuteDeleteAsync();

        if (affectedRows == 0)
        {
            throw new ServiceException("EntityNotFound");
        }
    }

    public async Task<ICollection<EmployeeShortGetDto>> GetAll(
        int skip = 0, int take = 10)
    {
        try
        {
            var employees = await _context.Employee
                .Where(entity => entity.IsActive)
                .Select(entity => new EmployeeShortGetDto
                {
                    Id = entity.Id,
                    FullName = entity.FullName,
                })
                .OrderBy(entity => entity.FullName)
                .Skip(skip).Take(take)
                .ToListAsync();

            return employees;
        }
        catch (InvalidOperationException ex)
        {
            throw new ServiceException("EntityNotFound", ex);
        }
    }

    public void Update(int id, EmployeeUpdateDto dtoEntity)
    {

    }

    // Used by: HTTP Basic Authentication Middleware
    public async Task<(int, string)> ValidateEmployee(string username, string password)
    {
        try
        {
            var employee = await _context.Employee
                .Where(entity => entity.Username == username)
                .Select(entity => new
                {
                    entity.Id,
                    Role = entity.Role.Name,
                    entity.PasswordHash
                })
                .FirstAsync();

            if (!_cryptographyService.VerifyPassword(password, employee.PasswordHash))
            {
                throw new ServiceException("InvalidUsernamePassword");
            }

            return (employee.Id, employee.Role);
        }
        catch (InvalidOperationException ex)
        {
            throw new ServiceException("EntityNotFound", ex);
        }
    }

    public async Task ForgotPassword(EmployeeForgotPasswowrdDto dtoEntity)
    {
        try
        {
            var employee = await _context.Employee
                .Where(entity => entity.Email == dtoEntity.Email)
                .Select(entity => new { entity.Id, entity.FullName })
                .FirstAsync();

            var emailValidationToken = _jwtService.GenerateToken(
                employee.Id, accessTokenExpirationHours: 1,
                additionalClaims: new Claim(ClaimTypes.Email, dtoEntity.Email)
            );

            // Email
            (string subject, string body) = _emailTemplateLoader.GetTemplate(
                EmailTemplateTypes.ForgotPassword,
                employee.FullName,
                emailValidationToken[0]
            );

            await _emailService.SendEmailAsync(
                [dtoEntity.Email], subject, body
            );
        }
        catch (InvalidOperationException ex)
        {
            throw new ServiceException("NotFound", ex);
        }
    }

    public async Task ResetPassword(string jwt, EmployeeResetPasswordDto dtoEntity)
    {
        string? id = _jwtService.ValidateTokenAndGetClaims(jwt)
            .Find(claim => claim.Type == ClaimTypes.NameIdentifier)
            ?.Value;
        string? emailAddress = _jwtService.ValidateTokenAndGetClaims(jwt)
            .Find(claim => claim.Type == ClaimTypes.Email)
            ?.Value;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(emailAddress))
        {
            throw new ServiceException("InvalidJwt");
        }

        int affectedRows = await _context.Employee
            .Where(entity => entity.Id == int.Parse(id))
            .ExecuteUpdateAsync(entity => entity
                .SetProperty(e => e.PasswordHash,
                    _cryptographyService.HashPassword(dtoEntity.Password)
                )
            );

        if (affectedRows == 0)
        {
            throw new ServiceException("EntityNotFound");
        }

        // Email
        (string subject, string body) = _emailTemplateLoader.GetTemplate(
            EmailTemplateTypes.ResetPasswordSuccessful
        );

        await _emailService.SendEmailAsync(
            [emailAddress], subject, body
        );
    }

    public async Task SetPassword(int id, EmployeeSetPasswordDto dtoEntity)
    {
        try
        {
            var employee = await _context.Employee
                .Where(entity => entity.Id == id)
                .FirstAsync();

            if (!_cryptographyService.VerifyPassword(
                dtoEntity.OldPassword, employee.PasswordHash))
            {
                throw new ServiceException("InvalidOldPassword");
            }

            employee.PasswordHash = _cryptographyService.HashPassword(
                dtoEntity.NewPassword);

            await _context.SaveChangesAsync();
        }
        catch (InvalidOperationException ex)
        {
            throw new ServiceException("", ex);
        }
    }

    public async Task UpdateEmail(EmployeeEmailUpdateDto dtoEntity)
    {
        var employee = await _context.Employee
            .Where(entity => entity.Id == dtoEntity.EmployeeId)
            .FirstAsync();

        if (employee.IsActive == false)
        {
            throw new ServiceException("InactiveAccount");
        }

        if (employee.Email == dtoEntity.Email)
        {
            throw new ServiceException("SameEmail");
        }

        if (await _context.Employee
            .Where(entity =>
                entity.Id != dtoEntity.EmployeeId && entity.Email == dtoEntity.Email
            ).AnyAsync()
        )
        {
            throw new ServiceException("EmailInUse");
        }

        employee.IsActive = false;

        await _context.SaveChangesAsync();

        var emailValidationToken = _jwtService.GenerateToken(
            employee.Id,
            accessTokenExpirationHours: 1,
            additionalClaims: new Claim(ClaimTypes.Email, dtoEntity.Email)
        );

        // Email
        (string subject, string body) = _emailTemplateLoader.GetTemplate(
            EmailTemplateTypes.ChangeEmail, employee.FullName, emailValidationToken[0]
        );

        await _emailService.SendEmailAsync(
            [dtoEntity.Email], subject, body
        );
    }

    public async Task FinishEmailUpdate(string jwt)
    {
        var claims = _jwtService.ValidateTokenAndGetClaims(jwt);

        string? id = claims.Find(
            claim => claim.Type == ClaimTypes.NameIdentifier
        )?.Value;
        string? email = claims.Find(
            claim => claim.Type == ClaimTypes.Email
        )?.Value;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(email))
        {
            throw new ServiceException("InvalidJwt");
        }

        int affectedRows = await _context.Employee
            .Where(entity => entity.Id == int.Parse(id))
            .ExecuteUpdateAsync(entity => entity
                .SetProperty(e => e.Email, email)
                .SetProperty(e => e.IsActive, true)
            );

        if (affectedRows == 0)
        {
            throw new ServiceException("EntityNotFound");
        }
    }

    // Profile Section
    public async Task<EmployeeGetDto> GetById(int id)
    {
        try
        {
            return await _context.Employee
                .Where(entity => entity.Id == id)
                .Select(entity => new EmployeeGetDto
                {
                    Id = entity.Id,
                    FullName = entity.FullName,
                    Email = entity.Email,
                    Username = entity.Username,
                    Phone = entity.Phone,
                    Role = entity.Role.Name,

                    Department = new DepartmentShortGetDto
                    {
                        Id = entity.Department.Id,
                        Name = entity.Department.Name,
                    },
                    Designation = new DesignationShortGetDto
                    {
                        Id = entity.Designation.Id,
                        Name = entity.Designation.Name,
                    },

                    IsActive = entity.IsActive,
                    CreatedOn = entity.CreatedOn,
                    UpdatedOn = entity.UpdatedOn,
                })
                .FirstAsync();
        }
        catch (InvalidOperationException)
        {
            throw new ServiceException("InvalidUsername");
        }
    }
}
