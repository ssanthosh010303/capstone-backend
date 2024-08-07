/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
using Microsoft.EntityFrameworkCore;

using WebApi.Models;

namespace WebApi.Contexts.Seeders;

public static class DesignationSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<DesignationModel>().HasData(
            new DesignationModel
            {
                Id = 1,
                Name = "Employee",
                EscalationLevel = 100
            },
            new DesignationModel
            {
                Id = 2,
                Name = "HR Manager",
                EscalationLevel = 200
            },
            new DesignationModel
            {
                Id = 3,
                Name = "Senior HR Manager",
                EscalationLevel = 300
            },
            new DesignationModel
            {
                Id = 4,
                Name = "Grievance Manager",
                EscalationLevel = 400
            },
            new DesignationModel
            {
                Id = 5,
                Name = "Aduitor",
                EscalationLevel = 500
            }
        );
    }
}
