/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DataTransferObjects;

public class DesignationEmployeesListDto
{
    public ICollection<EmployeeShortGetDto> Employees { get; set; }
}

public class DesignationGetDto : BaseGetDto
{
    public string Name { get; set; }

    public int EscalationLevel { get; set; }
}

public class DesignationShortGetDto : BaseShortGetDto
{
    public string Name { get; set; }

    public int EscalationLevel { get; set; }
}

public class DesignationAddRangeBaseDto
{
    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int EscalationLevel { get; set; }

    public DesignationModel Clone()
    {
        return new DesignationModel
        {
            Name = Name,
            EscalationLevel = EscalationLevel
        };
    }
}

public class DesignationAddRangeDto
{
    public ICollection<DesignationAddRangeBaseDto> Designations { get; set; }

    public ICollection<DesignationModel> Clone()
    {
        ICollection<DesignationModel> entities = [];

        foreach (var dto in Designations)
        {
            entities.Add(dto.Clone());
        }

        return entities;
    }
}

public class DesignationUpdateDto
{
    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int EscalationLevel { get; set; }

    public DesignationModel Update(DesignationModel entity)
    {
        entity.Name = Name;
        entity.EscalationLevel = EscalationLevel;

        return entity;
    }
}
