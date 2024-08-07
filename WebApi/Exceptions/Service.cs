/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
namespace WebApi.Exceptions;

public class ServiceException : Exception
{
    public ServiceException()
    {
    }

    public ServiceException(string message)
        : base(message)
    {
    }

    public ServiceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
