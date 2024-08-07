/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DataTransferObjects;

public class DepartmentEmployeesListDto
{
    public ICollection<EmployeeShortGetDto> Employees { get; set; }
}

public class DepartmentGetDto : BaseGetDto
{
    public string Name { get; set; }
}

public class DepartmentShortGetDto : BaseShortGetDto
{
    public string Name { get; set; }
}

public class DepartmentAddRangeDto
{
    [Required]
    public ICollection<string> Departments { get; set; }

    public ICollection<DepartmentModel> Clone()
    {
        ICollection<DepartmentModel> entities = [];

        foreach (var department in Departments)
        {
            if (department.Length > 32)
                throw new ValidationException("MaxLengthExceeded");

            entities.Add(new() { Name = department });
        }

        return entities;
    }
}

public class DepartmentUpdateDto
{
    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    public DepartmentModel Update(DepartmentModel entity)
    {
        entity.Name = Name;

        return entity;
    }
}
