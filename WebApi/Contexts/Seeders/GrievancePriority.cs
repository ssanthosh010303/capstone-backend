/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
using Microsoft.EntityFrameworkCore;

using WebApi.Models;

namespace WebApi.Contexts.Seeders;

public static class GrievancePrioritySeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<GrievancePriorityModel>().HasData(
            new GrievancePriorityModel
            {
                Id = 1,
                Name = "Low",
                PriorityLevel = 100
            },
            new GrievancePriorityModel
            {
                Id = 2,
                Name = "Medium",
                PriorityLevel = 200
            },
            new GrievancePriorityModel
            {
                Id = 3,
                Name = "High",
                PriorityLevel = 300
            }
        );
    }
}
