/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models;

[Index(nameof(BlobName), IsUnique = true)]
public class FileAttachmentModel : BaseModel
{
    [Required]
    public string BlobName { get; set; }

    [Required]
    [MaxLength(64)]
    public string Title { get; set; }

    // Grievance
    public int SubmittedWithGrievanceId { get; set; }

    public GrievanceModel SubmittedWithGrievance { get; set; }
}
