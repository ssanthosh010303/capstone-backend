/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DataTransferObjects;

public class GrievanceSubcategoryGrievanceListDto
{
    public ICollection<GrievanceShortGetDto> Grievances { get; set; }
}

public class GrievanceSubcategoryGetDto : BaseGetDto
{
    public string Name { get; set; }

    public GrievanceCategoryShortGetDto GrievanceCategory { get; set; }
}

public class GrievanceSubcategoryShortGetDto : BaseShortGetDto
{
    public string Name { get; set; }
}

public class GrievanceSubcategoryAddRangeDto
{
    [Required]
    public ICollection<string> GrievanceSubcategories { get; set; }

    public ICollection<GrievanceSubcategoryModel> Clone()
    {
        ICollection<GrievanceSubcategoryModel> entities = [];

        foreach (var subcategory in GrievanceSubcategories)
        {
            if (subcategory.Length > 32)
                throw new ValidationException("MaxLengthExceeded");

            entities.Add(new() { Name = subcategory });
        }

        return entities;
    }
}

public class GrievanceSubcategoryUpdateDto
{
    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    public GrievanceSubcategoryModel Update(GrievanceSubcategoryModel entity)
    {
        entity.Name = Name;

        return entity;
    }
}
