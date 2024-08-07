/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DataTransferObjects;

public class GrievanceStatusGrievanceListDto
{
    public ICollection<GrievanceShortGetDto> Grievances { get; set; }
}

public class GrievanceStatusGetDto : BaseGetDto
{
    public string Name { get; set; }
}

public class GrievanceStatusShortGetDto : BaseShortGetDto
{
    public string Name { get; set; }
}

public class GrievanceStatusAddRangeDto
{
    [Required]
    public ICollection<string> Statuses { get; set; }

    public ICollection<GrievanceStatusModel> Clone()
    {
        ICollection<GrievanceStatusModel> entities = [];

        foreach (var status in Statuses)
        {
            if (status.Length > 32)
                throw new ValidationException("MaxLengthExceeded");

            entities.Add(new() { Name = status });
        }

        return entities;
    }
}

public class GrievanceStatusUpdateDto
{
    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    public GrievanceStatusModel Update(GrievanceStatusModel entity)
    {
        entity.Name = Name;

        return entity;
    }
}
