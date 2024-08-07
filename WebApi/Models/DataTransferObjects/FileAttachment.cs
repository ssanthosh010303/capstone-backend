/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DataTransferObjects;

public class FileAttachmentShortGetDto : BaseShortGetDto
{
    public string BlobName { get; set; }

    public string Title { get; set; }
}

public class AzureBlobGetUploadUrlsDto
{
    [Required]
    public int SubmittedWithGrievanceId { get; set; }

    public ICollection<string> FileExtensions { get; set; }
}

public class FileAttachmentAddBaseDto
{
    [Required]
    public string BlobName { get; set; }

    [Required]
    public string Title { get; set; }
}

public class FileAttachmentsAddDto
{
    [Required]
    public int SubmittedWithGrievanceId { get; set; }

    public ICollection<FileAttachmentAddBaseDto> FileAttachments { get; set; }
}
