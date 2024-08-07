/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models;

[Index(nameof(Name), IsUnique = true)]
[Index(nameof(PriorityLevel), IsUnique = true)]
public class GrievancePriorityModel : BaseModel
{
    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int PriorityLevel { get; set; }

    public ICollection<GrievanceModel> Grievances { get; set; }
}
