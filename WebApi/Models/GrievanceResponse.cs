/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public enum GrievanceResponseMessageType
{
    Message,
    StatusChange,
    PriorityChange,
    AssigneeChange,
    Escalation,
    DeadlineChange,
}

public class GrievanceResponseModel : BaseModel
{
    [Required]
    public string Description { get; set; }

    // Grievance
    public int GrievanceId { get; set; }

    public GrievanceModel Grievance { get; set; }

    // Creator
    public int CreatedById { get; set; }

    public EmployeeModel CreatedBy { get; set; }

    // Message Type
    public GrievanceResponseMessageType MessageType { get; set; }
}
