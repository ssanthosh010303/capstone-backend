/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
namespace WebApi.Models.DataTransferObjects;

public class BaseShortGetDto
{
    public int Id { get; set; }
}

public class BaseGetDto
{
    public int Id { get; set; }

    public bool IsActive { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}
