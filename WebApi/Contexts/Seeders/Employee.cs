/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
using Microsoft.EntityFrameworkCore;

using WebApi.Models;
using WebApi.Services;

namespace WebApi.Contexts.Seeders;

public static class EmployeeSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        var cryptoService = new CryptographyService();

        builder.Entity<EmployeeModel>().HasData(
            new EmployeeModel
            {
                Id = 1,
                FullName = "Admin",
                Email = "sakthisanthosh010303+admin@gmail.com",
                Username = "admin",
                Phone = "+1 42547 45247",
                PasswordHash = cryptoService.HashPassword("admin@1234"),
                DepartmentId = 1,
                DesignationId = 1,
                RoleId = 1
            },
            new EmployeeModel
            {
                Id = 2,
                FullName = "Manager-1",
                Email = "sakthisanthosh010303+manager1@gmail.com",
                Username = "manager1",
                Phone = "+1 42547 45607",
                PasswordHash = cryptoService.HashPassword("manager1@1234"),
                DepartmentId = 1,
                DesignationId = 2,
                RoleId = 2
            },
            new EmployeeModel
            {
                Id = 3,
                FullName = "Manager-2",
                Email = "sakthisanthosh010303+manager2@gmail.com",
                Username = "manager2",
                Phone = "+1 42547 45640",
                PasswordHash = cryptoService.HashPassword("manager2@1234"),
                DepartmentId = 1,
                DesignationId = 2,
                RoleId = 2
            },
            new EmployeeModel
            {
                Id = 4,
                FullName = "Manager-3",
                Email = "sakthisanthosh010303+manager3@gmail.com",
                Username = "manager3",
                Phone = "+1 42547 45647",
                PasswordHash = cryptoService.HashPassword("manager3@1234"),
                DepartmentId = 1,
                DesignationId = 3,
                RoleId = 2
            },
            new EmployeeModel
            {
                Id = 5,
                FullName = "Sakthi Santhosh",
                Email = "sakthisanthosh010303@gmail.com",
                Username = "sakthisanthosh",
                Phone = "+1 42547 45248",
                PasswordHash = cryptoService.HashPassword("sakthisanthosh@1234"),
                DepartmentId = 1,
                DesignationId = 1,
                RoleId = 3
            }
        );
    }
}
