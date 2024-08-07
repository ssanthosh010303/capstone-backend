/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
using Microsoft.EntityFrameworkCore;

using WebApi.Models;

namespace WebApi.Contexts.Seeders;

public static class DepartmentSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<DepartmentModel>().HasData(
            new DepartmentModel
            {
                Id = 1,
                Name = "Application Management"
            }
        );
    }
}
