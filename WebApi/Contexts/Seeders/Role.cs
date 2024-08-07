/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
using Microsoft.EntityFrameworkCore;

using WebApi.Models;

namespace WebApi.Contexts.Seeders;

public static class RoleSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<RoleModel>().HasData(
            new RoleModel
            {
                Id = 1,
                Name = "Admin"
            },
            new RoleModel
            {
                Id = 2,
                Name = "Manager"
            },
            // XXX: Change Causes Breakage
            new RoleModel
            {
                Id = 3,
                Name = "User"
            },
            new RoleModel
            {
                Id = 4,
                Name = "Microservice"
            }
        );
    }
}
