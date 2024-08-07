/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
using Microsoft.EntityFrameworkCore;

using WebApi.Exceptions;

namespace WebApi.Utils;

public class DbExceptionHandler
{
    public static void ThrowDetailedException(DbUpdateException ex)
    {
        if (ex.InnerException is MySqlConnector.MySqlException sqlException)
        {
            switch (sqlException.Number)
            {
                case 1452:
                    throw new ServiceException("ForeignKeyInvalid");
                case 1062:
                    throw new ServiceException("DuplicateEntry");
                default:
                    throw new ServiceException(ex.Message);
            }
        }
        else
        {
            throw new ServiceException("UnknownError");
        }
    }
}
