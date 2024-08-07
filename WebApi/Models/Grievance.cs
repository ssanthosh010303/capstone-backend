/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class GrievanceModel : BaseModel
{
    [Required]
    public DateTime Deadline { get; set; }

    [Required]
    [MaxLength(256)]
    public string Title { get; set; }

    public bool IsAnonymous { get; set; }

    // Status
    public int GrievanceStatusId { get; set; }

    public GrievanceStatusModel GrievanceStatus { get; set; }

    // Priority
    public int GrievancePriorityId { get; set; }

    public GrievancePriorityModel GrievancePriority { get; set; }

    // Subcategory
    public int GrievanceSubcategoryId { get; set; }

    public GrievanceSubcategoryModel GrievanceSubcategory { get; set; }

    // Creator
    public int CreatedById { get; set; }

    public EmployeeModel CreatedBy { get; set; }

    // Assignee
    public int? AssignedToId { get; set; }

    public EmployeeModel AssignedTo { get; set; }

    // M2M
    public ICollection<GrievanceResponseModel> Responses { get; set; }

    public ICollection<MeetingModel> Meetings { get; set; }

    public ICollection<EmployeeModel> RelatedEmployees { get; set; }

    public ICollection<FileAttachmentModel> FileAttachments { get; set; }
}
