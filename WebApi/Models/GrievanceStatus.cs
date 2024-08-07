/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models;

[Index(nameof(Name), IsUnique = true)]
public class GrievanceStatusModel : BaseModel
{
    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    public ICollection<GrievanceModel> Grievances { get; set; }
}
