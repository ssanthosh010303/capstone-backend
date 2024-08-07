/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DataTransferObjects;

public class EmployeeGetDto : BaseGetDto
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Username { get; set; }

    public string Phone { get; set; }

    public string Role { get; set; }

    public DepartmentShortGetDto Department { get; set; } = new();

    public DesignationShortGetDto Designation { get; set; } = new();
}

public class EmployeeShortGetDto : BaseShortGetDto
{
    public string FullName { get; set; }
}

public class EmployeeAddDto
{
    [Required]
    [Display(Name = "Name")]
    [MaxLength(64)]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(64)]
    public string Username { get; set; }

    [Required]
    [Phone]
    [MaxLength(15)]
    public string Phone { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }

    public int DepartmentId { get; set; }

    public int DesignationId { get; set; }

    public EmployeeModel Clone(EmployeeModel entity)
    {
        entity.FullName = FullName;
        entity.Email = Email;
        entity.Username = Username;
        entity.Phone = Phone;
        entity.DepartmentId = DepartmentId;
        entity.PasswordHash = Password;
        entity.DesignationId = DesignationId;

        return entity;
    }
}

public class EmployeeUpdateDto
{
    [Required]
    [Display(Name = "Name")]
    [MaxLength(64)]
    public string FullName { get; set; }

    [Required]
    [MaxLength(64)]
    public string Username { get; set; }

    public int DepartmentId { get; set; }

    public int DesignationId { get; set; }

    public EmployeeModel Clone(EmployeeModel entity)
    {
        entity.FullName = FullName;
        entity.Username = Username;
        entity.DepartmentId = DepartmentId;
        entity.DesignationId = DesignationId;

        return entity;
    }
}

public class EmployeeForgotPasswowrdDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

public class EmployeeEmailUpdateDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public int EmployeeId { get; set; }
}

public class EmployeeResetPasswordDto
{
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}

public class EmployeeSetPasswordDto
{
    [Required]
    public string OldPassword { get; set; }

    [Required]
    [MinLength(8)]
    public string NewPassword { get; set; }
}
