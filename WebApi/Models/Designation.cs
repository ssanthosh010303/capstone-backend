/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models;

// Unique Separately and Compounded
[Index(nameof(Name), IsUnique = true)]
[Index(nameof(EscalationLevel), IsUnique = true)]
public class DesignationModel : BaseModel
{
    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int EscalationLevel { get; set; }

    public ICollection<EmployeeModel> Employees { get; set; }
}
