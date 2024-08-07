/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DataTransferObjects;

public class GrievanceResponseShortGetDto
{
    public int Id { get; set; }

    public string Description { get; set; }

    public GrievanceResponseMessageType MessageType { get; set; }

    public int CreatedById { get; set; }

    public DateTime CreatedOn { get; set; }
}

public class GrievanceResponseAddDto
{
    [Required]
    public int GrievanceId { get; set; }

    [Required]
    public string Description { get; set; }

    public GrievanceResponseModel Clone(GrievanceResponseModel entity)
    {
        entity.Description = Description;
        entity.GrievanceId = GrievanceId;

        return entity;
    }
}
