/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DataTransferObjects;

public class MeetingShortGetDto : BaseGetDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime Date { get; set; }

    public TimeOnly Duration { get; set; }

    public int GrievanceId { get; set; }
}

public class MeetingGetDto : BaseGetDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime Date { get; set; }

    public TimeOnly Duration { get; set; }

    public GrievanceShortGetDto Grievance { get; set; }

    public EmployeeShortGetDto Attendee { get; set; }
}

public class EmployeeMeetingGetBaseDto : BaseShortGetDto
{
    public string Title { get; set; }

    public DateTime Date { get; set; }



    public TimeOnly Duration { get; set; }

    public int GrievanceId { get; set; }
}

public class EmployeeMeetingGetDto
{
    public ICollection<EmployeeMeetingGetBaseDto> Meetings { get; set; }
}

public class MeetingAddDto
{
    [Required]
    [MaxLength(256)]
    public string Title { get; set; }

    public string Description { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public TimeOnly Duration { get; set; }

    public int GrievanceId { get; set; }

    public int AttendeeId { get; set; }

    public MeetingModel Clone(MeetingModel entity)
    {
        entity.Title = Title;
        entity.Description = Description;
        entity.Date = Date;
        entity.Duration = Duration;
        entity.GrievanceId = GrievanceId;
        entity.AttendeeId = AttendeeId;

        return entity;
    }
}

public class MeetingUpdateDto
{

}
