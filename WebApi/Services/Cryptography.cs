/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 25/07/2024
 */
using System.Security.Cryptography;

namespace WebApi.Services;

public interface ICryptographyService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}

public class CryptographyService : ICryptographyService
{
    private readonly int saltSize = 16;
    private readonly int hashSize = 20;
    private readonly int iterations = 10000;

    public string HashPassword(string password)
    {
        byte[] salt;

        RandomNumberGenerator.Fill(salt = new byte[saltSize]);

        var pbkdf2 = new Rfc2898DeriveBytes(
            password, salt, iterations, HashAlgorithmName.SHA256
        );

        byte[] hash = pbkdf2.GetBytes(hashSize);
        byte[] hashBytes = new byte[saltSize + hashSize];

        Array.Copy(salt, 0, hashBytes, 0, saltSize);
        Array.Copy(hash, 0, hashBytes, saltSize, hashSize);

        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);
        byte[] salt = new byte[saltSize];

        Array.Copy(hashBytes, 0, salt, 0, saltSize);

        var pbkdf2 = new Rfc2898DeriveBytes(
            password, salt, iterations, HashAlgorithmName.SHA256
        );

        byte[] hash = pbkdf2.GetBytes(hashSize);

        for (int i = 0; i < hashSize; i++)
        {
            if (hashBytes[i + saltSize] != hash[i])
            {
                return false;
            }
        }

        return true;
    }
}
