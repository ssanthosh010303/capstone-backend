/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class GrievanceCategoryModel : BaseModel
{
    [Required]
    [MaxLength(64)]
    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<GrievanceSubcategoryModel> GrievanceSubcategories { get; set; }
}
