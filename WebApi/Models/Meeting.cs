/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class MeetingModel : BaseModel
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

    public GrievanceModel Grievance { get; set; }

    public int CreatedById { get; set; }

    public EmployeeModel CreatedBy { get; set; }

    public int AttendeeId { get; set; }

    public EmployeeModel Attendee { get; set; }
}
