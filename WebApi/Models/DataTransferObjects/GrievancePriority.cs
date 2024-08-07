/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DataTransferObjects;

public class GrievancePriorityGrievanceListDto
{
    public ICollection<GrievanceShortGetDto> Grievances { get; set; }
}

public class GrievancePriorityGetDto : BaseGetDto
{
    public string Name { get; set; }

    public int PriorityLevel { get; set; }
}

public class GrievancePriorityShortGetDto : BaseShortGetDto
{
    public string Name { get; set; }

    public int PriorityLevel { get; set; }
}

public class GrievancePriorityAddRangeBaseDto
{
    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int PriorityLevel { get; set; }

    public GrievancePriorityModel Clone()
    {
        return new GrievancePriorityModel
        {
            Name = Name,
            PriorityLevel = PriorityLevel
        };
    }
}

public class GrievancePriorityAddRangeDto
{
    public ICollection<GrievancePriorityAddRangeBaseDto> GrievancePriorities { get; set; }

    public ICollection<GrievancePriorityModel> Clone()
    {
        ICollection<GrievancePriorityModel> entities = [];

        foreach (var dto in GrievancePriorities)
        {
            entities.Add(dto.Clone());
        }

        return entities;
    }
}

public class GrievancePriorityUpdateDto
{
    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int PriorityLevel { get; set; }

    public GrievancePriorityModel Update(GrievancePriorityModel entity)
    {
        entity.Name = Name;
        entity.PriorityLevel = PriorityLevel;

        return entity;
    }
}
