/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
namespace WebApi.Contexts.Seeders;

using Microsoft.EntityFrameworkCore;

using WebApi.Models;

public static class GrievanceStatusSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<GrievanceStatusModel>().HasData(
            new GrievanceStatusModel
            {
                // XXX: Change Causes Breakage
                Id = 1,
                Name = "Pending"
            },
            new GrievanceStatusModel
            {
                Id = 2,
                Name = "Approved"
            },
            new GrievanceStatusModel
            {
                Id = 3,
                Name = "Rejected"
            },
            new GrievanceStatusModel
            {
                Id = 4,
                Name = "Under Review"
            },
            new GrievanceStatusModel
            {
                Id = 5,
                Name = "Resolved"
            },
            new GrievanceStatusModel
            {
                Id = 6,
                Name = "Escalated"
            },
            new GrievanceStatusModel
            {
                Id = 7,
                Name = "Withdrawn"
            }
        );
    }
}
