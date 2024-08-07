/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DataTransferObjects;

public class GrievanceCategoryGrievanceSubcategoryListDto
{
    public ICollection<GrievanceSubcategoryShortGetDto> GrievanceSubcategories { get; set; }
}

public class GrievanceCategoryGetDto : BaseGetDto
{
    public string Name { get; set; }

    public string Description { get; set; }
}

public class GrievanceCategoryShortGetDto : BaseShortGetDto
{
    public string Name { get; set; }

    public string Description { get; set; }
}

public class GrievanceCategoryAddRangeBaseDto
{
    [Required]
    [MaxLength(64)]
    public string Name { get; set; }

    public string Description { get; set; }
}

public class GrievanceCategoryAddRangeDto
{
    public ICollection<GrievanceCategoryAddRangeBaseDto> GrievanceCategories { get; set; }

    public ICollection<GrievanceCategoryModel> Clone()
    {
        ICollection<GrievanceCategoryModel> entities = [];

        foreach (var dto in GrievanceCategories)
        {
            entities.Add(new GrievanceCategoryModel
            {
                Name = dto.Name,
                Description = dto.Description
            });
        }

        return entities;
    }
}

public class GrievanceCategoryUpdateDto
{
    [Required]
    [MaxLength(64)]
    public string Name { get; set; }

    public string Description { get; set; }

    public GrievanceCategoryModel Update(GrievanceCategoryModel entity)
    {
        entity.Name = Name;
        entity.Description = Description;

        return entity;
    }
}
