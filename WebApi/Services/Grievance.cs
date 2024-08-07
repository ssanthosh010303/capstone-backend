/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
using Microsoft.EntityFrameworkCore;

using WebApi.Contexts;
using WebApi.Email;
using WebApi.Exceptions;
using WebApi.Models.DataTransferObjects;
using WebApi.Utils;

namespace WebApi.Services;

public interface IGrievanceService
{
    Task Add(int employeeId, GrievanceAddUpdateDto entity);

    Task<ICollection<GrievanceShortGetDto>> GetAll(int employeeId, int skip, int take, bool isAssignee);
    Task<GrievanceGetDto> GetById(int employeeId, int grievanceId);

    Task SetStatus(int employeeId, int grievanceId, int statusId);
    Task Escalate(int grievanceId);
}

public class GrievanceService(
    ApplicationDbContext context,
    IEmailTemplateLoader emailTemplateLoader,
    IEmailService emailService) : IGrievanceService
{
    private readonly ApplicationDbContext _context = context;

    private readonly IEmailTemplateLoader _emailTemplateLoader = emailTemplateLoader;
    private readonly IEmailService _emailService = emailService;

    public async Task Add(int employeeId, GrievanceAddUpdateDto dtoEntity)
    {
        try
        {
            // Validations
            if (dtoEntity.Deadline < DateTime.Now.AddDays(3))
            {
                throw new ServiceException("DeadlineTooClose");
            }

            var newGrievance = dtoEntity.Clone(new());

            if (GetOpenGrievancesCount(employeeId) > 5)
            {
                throw new ServiceException("TooManyOpenGrievances");
            }

            // Set Defaults
            newGrievance.CreatedById = employeeId;
            newGrievance.GrievanceStatusId = 1; // XXX: Hardcoding

            // Set Assignee: Round-Robin Assignment
            var assignee = await _context.Employee
                .Where(entity => entity.IsActive
                    && entity.Designation.EscalationLevel >= 200) // XXX: Hardcoding
                .OrderBy(entity => entity.OpenGrievanceCount)
                .FirstAsync();

            assignee.OpenGrievanceCount++;
            newGrievance.AssignedToId = assignee.Id;

            _context.Employee.Update(assignee);
            await _context.Grievance.AddAsync(newGrievance);
            await _context.SaveChangesAsync();

            // Email
            string employeeEmail = _context.Employee
                .Where(entity => entity.Id == employeeId)
                .Select(entity => entity.Email)
                .First();

            (string subject, string body) = _emailTemplateLoader.GetTemplate(
                EmailTemplateTypes.GrievanceCreatedEmployee,
                newGrievance.Title,
                newGrievance.CreatedOn.ToString("dd/MM/yyyy"),
                newGrievance.Deadline.ToString("dd/MM/yyyy")
            );

            await _emailService.SendEmailAsync([employeeEmail], subject, body);

            string managerEmail = _context.Employee
                .Where(entity => entity.Id == employeeId)
                .Select(entity => entity.Email)
                .First();

            (subject, body) = _emailTemplateLoader.GetTemplate(
                EmailTemplateTypes.GrievanceCreatedManager,
                newGrievance.Title,
                newGrievance.CreatedOn.ToString("dd/MM/yyyy"),
                newGrievance.Deadline.ToString("dd/MM/yyyy")
            );

            await _emailService.SendEmailAsync([managerEmail], subject, body);
        }
        catch (DbUpdateException ex)
        {
            DbExceptionHandler.ThrowDetailedException(ex);
        }
    }

    public async Task<ICollection<GrievanceShortGetDto>> GetAll(
        int employeeId, int skip = 0, int take = 10, bool isAssignee = false)
    {
        try
        {
            if (isAssignee)
            {
                return await _context.Grievance
                    .Where(entity => entity.AssignedToId == employeeId
                        && entity.IsActive
                    )
                    .Select(entity => new GrievanceShortGetDto
                    {
                        Id = entity.Id,
                        Title = entity.Title,
                        Deadline = entity.Deadline,
                        Status = entity.GrievanceStatus.Name
                    })
                    .OrderByDescending(entity => entity.Deadline)
                    .Skip(skip).Take(take)
                    .ToListAsync();
            }
            else
            {
                return await _context.Grievance
                    .Where(entity => entity.CreatedById == employeeId
                        && entity.IsActive
                    )
                    .Select(entity => new GrievanceShortGetDto
                    {
                        Id = entity.Id,
                        Title = entity.Title,
                        Deadline = entity.Deadline,
                        Status = entity.GrievanceStatus.Name
                    })
                    .OrderByDescending(entity => entity.Deadline)
                    .Skip(skip).Take(take)
                    .ToListAsync();
            }
        }
        catch (ArgumentNullException ex)
        {
            throw new ServiceException("Invalid", ex);
        }
    }

    public async Task<GrievanceGetDto> GetById(int employeeId, int grievanceId)
    {
        try
        {
            // Condition: Return Only If Grievance is Active, ID Matches Creator or Assignee
            return await _context.Grievance
                .Where(entity => entity.Id == grievanceId
                    && (
                        entity.CreatedById == employeeId || entity.AssignedToId == employeeId
                    ) && entity.IsActive
                )
                .Select(entity => new GrievanceGetDto
                {
                    Id = entity.Id,
                    Deadline = entity.Deadline,
                    Title = entity.Title,
                    IsAnonymous = entity.IsAnonymous,
                    GrievanceStatus = new GrievanceStatusShortGetDto
                    {
                        Id = entity.GrievanceStatus.Id,
                        Name = entity.GrievanceStatus.Name
                    },
                    GrievancePriority = new GrievancePriorityShortGetDto
                    {
                        Id = entity.GrievancePriority.Id,
                        Name = entity.GrievancePriority.Name,
                        PriorityLevel = entity.GrievancePriority.PriorityLevel
                    },
                    GrievanceSubcategory = new GrievanceSubcategoryShortGetDto
                    {
                        Id = entity.GrievanceSubcategory.Id,
                        Name = entity.GrievanceSubcategory.Name
                    },
                    CreatedBy = new EmployeeShortGetDto
                    {
                        Id = entity.CreatedBy.Id,
                        FullName = entity.CreatedBy.FullName
                    },
                    AssignedTo = entity.AssignedTo != null ? new EmployeeShortGetDto
                    {
                        Id = entity.AssignedTo.Id,
                        FullName = entity.AssignedTo.FullName
                    } : null,
                    FileAttachments = entity.FileAttachments
                        .Select(file => new FileAttachmentShortGetDto
                        {
                            Id = file.Id,
                            BlobName = file.BlobName,
                            Title = file.Title
                        })
                        .ToList(),

                    CreatedOn = entity.CreatedOn,
                    UpdatedOn = entity.UpdatedOn
                })
                .FirstAsync();
        }
        catch (InvalidOperationException ex)
        {
            throw new ServiceException("GrievanceNotFound", ex);
        }
    }

    public async Task SetStatus(int employeeId, int grievanceId, int statusId)
    {
        try
        {
            var grievance = await _context.Grievance
                .Where(entity => entity.Id == grievanceId)
                .FirstAsync();

            grievance.GrievanceStatusId = statusId; // XXX: Hardcoding
            grievance.UpdatedOn = DateTime.Now;

            _context.Grievance.Update(grievance);
            await _context.SaveChangesAsync();

            if (statusId == 6)
                await Escalate(grievanceId);
        }
        catch (InvalidOperationException ex)
        {
            throw new ServiceException("GrievanceNotFound", ex);
        }
    }

    public async Task Escalate(int grievanceId)
    {
        try
        {
            var grievance = await _context.Grievance
                .Where(entity => entity.Id == grievanceId)
                .Include(entity => entity.AssignedTo)
                .FirstAsync();

            var assignee = await _context.Employee
                .Where(entity => entity.IsActive
                    && entity.Designation.EscalationLevel >= 300) // XXX: Hardcoding
                .OrderBy(entity => entity.OpenGrievanceCount)
                .FirstAsync();

            grievance.AssignedTo.OpenGrievanceCount--;
            grievance.AssignedToId = assignee.Id;

            _context.Employee.Update(assignee);
            _context.Grievance.Update(grievance);
            await _context.SaveChangesAsync();

            // Send Email
            (string subject, string body) = _emailTemplateLoader.GetTemplate(
                EmailTemplateTypes.GrievanceAssigned,
                grievance.Title,
                grievance.CreatedOn.ToString("dd/MM/yyyy"),
                grievance.Deadline.ToString("dd/MM/yyyy")
            );

            await _emailService.SendEmailAsync([assignee.Email], subject, body);
        }
        catch (InvalidOperationException ex)
        {
            throw new ServiceException("GrievanceNotFound", ex);
        }
    }

    // Utility
    private int GetOpenGrievancesCount(int employeeId)
    {
        return _context.Grievance
            .Where(entity => entity.CreatedById == employeeId && entity.IsActive
                && entity.GrievanceStatusId == 1) // XXX: Hardcoding
            .Count();
    }
}
