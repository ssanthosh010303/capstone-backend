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

public interface IMeetingService
{
    Task Add(int employeeId, MeetingAddDto dtoEntity);
    Task Update(int id, MeetingUpdateDto dtoEntity);

    Task Delete(int id);

    Task<ICollection<MeetingShortGetDto>> GetAll(
        int userId, int skip, int take
    );
    Task<MeetingGetDto> GetById(int id);
}

public class MeetingService(ApplicationDbContext _context,
    IEmailTemplateLoader emailTemplateLoader,
    IEmailService emailService
) : IMeetingService
{
    private readonly ApplicationDbContext _context = _context;

    private readonly IEmailTemplateLoader _emailTemplateLoader = emailTemplateLoader;
    private readonly IEmailService _emailService = emailService;

    public async Task Add(int employeeId, MeetingAddDto dtoEntity)
    {
        await IsEmployeeAuthorized(
            employeeId, dtoEntity.GrievanceId
        );
        // await IsMeetingLimitReached(dtoEntity.GrievanceId);

        try
        {
            var entity = dtoEntity.Clone(new());

            entity.CreatedById = employeeId;

            await _context.Meeting.AddAsync(entity);
            await _context.SaveChangesAsync();

            // TODO: Send Email to Attendees
            string attendeeEmail = _context.Employee
                .Where(entity => entity.Id == dtoEntity.AttendeeId)
                .Select(entity => entity.Email)
                .First();

            (string subject, string body) = _emailTemplateLoader.GetTemplate(
                EmailTemplateTypes.MeetingScheduled,
                dtoEntity.Title,
                dtoEntity.Date.ToString(),
                dtoEntity.Duration.ToString()
            );

            await _emailService.SendEmailAsync([attendeeEmail], subject, body);
        }
        catch (DbUpdateException ex)
        {
            DbExceptionHandler.ThrowDetailedException(ex);
        }
    }

    public Task Delete(int id)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    // Returns: Upcoming Meetings for a Employee Paginated
    public async Task<ICollection<MeetingShortGetDto>> GetAll(
        int employeeId, int skip, int take)
    {
        try
        {
            return await _context.Meeting
                .Where(entity => entity.CreatedById == employeeId)
                .Select(entity => new MeetingShortGetDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Date = entity.Date,
                    Duration = entity.Duration,
                    GrievanceId = entity.GrievanceId
                }).ToListAsync();
        }
        catch (InvalidOperationException ex)
        {
            throw new ServiceException("EmployeeNotFound", ex);
        }
    }

    public async Task<MeetingGetDto> GetById(int id)
    {
        try
        {
            return await _context.Meeting
                .Where(entity => entity.Id == id)
                .Select(entity => new MeetingGetDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    Date = entity.Date,
                    Grievance = new GrievanceShortGetDto
                    {
                        Id = entity.Grievance.Id,
                        Title = entity.Grievance.Title,
                        Deadline = entity.Grievance.Deadline
                    },
                    Attendee = new EmployeeShortGetDto
                    {
                        Id = entity.Attendee.Id,
                        FullName = entity.Attendee.FullName
                    }
                })
                .FirstAsync();
        }
        catch (InvalidOperationException ex)
        {
            throw new ServiceException("MeetingNotFound", ex);
        }
    }

    public Task Update(int id, MeetingUpdateDto dtoEntity)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    // Utility
    private async Task IsEmployeeAuthorized(int employeeId, int grievanceId)
    {
        try
        {
            int assigneeId = await _context.Grievance
                .Where(entity => entity.Id == grievanceId)
                .Select(entity => entity.AssignedToId)
                .FirstAsync() ?? -1;

            if (assigneeId != employeeId || assigneeId == -1)
            {
                throw new ServiceException("Unauthorized");
            }
        }
        catch (InvalidOperationException ex)
        {
            throw new ServiceException("GrievanceNotFound", ex);
        }
    }

    private async Task IsMeetingLimitReached(int grievanceId)
    {
        try
        {
            int meetingCount = await _context.Grievance
                .Where(entity => entity.Id == grievanceId)
                .Select(entity => entity.Meetings.Count)
                .FirstAsync();

            if (meetingCount >= 3)
            {
                throw new ServiceException("MeetingLimitReached");
            }
        }
        catch (InvalidOperationException ex)
        {
            throw new ServiceException("GrievanceNotFound", ex);
        }
    }
}
