/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
#nullable disable

namespace WebApi.Models.DataTransferObjects;

public class JwtGetDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

public class JwtRefreshDto
{
    public string AccessToken { get; set; }
}
