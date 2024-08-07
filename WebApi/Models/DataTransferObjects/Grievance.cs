/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DataTransferObjects;

public class GrievanceGetDto : BaseGetDto
{
    public DateTime Deadline { get; set; }

    public string Title { get; set; }

    public bool IsAnonymous { get; set; }

    public GrievanceStatusShortGetDto GrievanceStatus { get; set; }

    public GrievancePriorityShortGetDto GrievancePriority { get; set; }

    public GrievanceSubcategoryShortGetDto GrievanceSubcategory { get; set; }

    public EmployeeShortGetDto CreatedBy { get; set; }

    public EmployeeShortGetDto AssignedTo { get; set; }

    public ICollection<FileAttachmentShortGetDto> FileAttachments { get; set; }
}

public class GrievanceShortGetDto : BaseShortGetDto
{
    public string Title { get; set; }

    public string Status { get; set; }

    public DateTime Deadline { get; set; }
}

public class GrievanceAddUpdateDto
{
    [Required]
    public DateTime Deadline { get; set; }

    [Required]
    [MaxLength(256)]
    public string Title { get; set; }

    public bool IsAnonymous { get; set; } = false;

    public int GrievancePriorityId { get; set; }

    public int GrievanceSubcategoryId { get; set; }

    public GrievanceModel Clone(GrievanceModel entity)
    {
        entity.Deadline = Deadline;
        entity.Title = Title;
        entity.IsAnonymous = IsAnonymous;
        entity.GrievancePriorityId = GrievancePriorityId;
        entity.GrievanceSubcategoryId = GrievanceSubcategoryId;

        return entity;
    }
}
