/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models;

[Index(nameof(Username), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Phone), IsUnique = true)]
public class EmployeeModel : BaseModel
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

    public int OpenGrievanceCount { get; set; } = 0;

    [Required]
    public string PasswordHash { get; set; }

    public int DepartmentId { get; set; }

    public DepartmentModel Department { get; set; }

    public int DesignationId { get; set; }

    public DesignationModel Designation { get; set; }

    public int RoleId { get; set; }

    public RoleModel Role { get; set; }

    // XXX: Only for M2M with `Grievance.RelatedEmployees`
    public ICollection<GrievanceModel> RelatedGrievances { get; set; }
}
