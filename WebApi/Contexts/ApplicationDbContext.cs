/*
 * Author: Apache X692 Attack Helicopter
 * Created on: 24/07/2024
 */
using Microsoft.EntityFrameworkCore;

using WebApi.Contexts.Seeders;
using WebApi.Models;

namespace WebApi.Contexts;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<DepartmentModel> Department { get; set; }
    public DbSet<RoleModel> Role { get; set; }
    public DbSet<EmployeeModel> Employee { get; set; }
    public DbSet<MeetingModel> Meeting { get; set; }
    public DbSet<DesignationModel> Designation { get; set; }
    public DbSet<OtpModel> Otp { get; set; }

    public DbSet<FileAttachmentModel> FileAttachment { get; set; }
    public DbSet<GrievanceModel> Grievance { get; set; }
    public DbSet<GrievancePriorityModel> GrievancePriority { get; set; }
    public DbSet<GrievanceResponseModel> GrievanceResponse { get; set; }
    public DbSet<GrievanceStatusModel> GrievanceStatus { get; set; }
    public DbSet<GrievanceCategoryModel> GrievanceCategory { get; set; }
    public DbSet<GrievanceSubcategoryModel> GrievanceSubcategory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GrievanceModel>()
            .HasMany(g => g.RelatedEmployees)
            .WithMany(e => e.RelatedGrievances)
            .UsingEntity<Dictionary<string, object>>(
                "GrievanceEmployee",
                j => j.HasOne<EmployeeModel>().WithMany()
                    .HasForeignKey("EmployeeId"),
                j => j.HasOne<GrievanceModel>().WithMany()
                    .HasForeignKey("GrievanceId"));

        modelBuilder.Entity<GrievanceModel>()
            .HasOne(g => g.AssignedTo) // Employee Related
            .WithMany() // Grievances
            .HasForeignKey(g => g.AssignedToId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<GrievanceModel>()
            .HasOne(g => g.CreatedBy)
            .WithMany()
            .HasForeignKey(g => g.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        DepartmentSeeder.Seed(modelBuilder);
        DesignationSeeder.Seed(modelBuilder);
        RoleSeeder.Seed(modelBuilder);
        GrievancePrioritySeeder.Seed(modelBuilder);
        GrievanceStatusSeeder.Seed(modelBuilder);
        GrievanceCategorySeeder.Seed(modelBuilder);
        GrievanceSubcategorySeeder.Seed(modelBuilder);
        EmployeeSeeder.Seed(modelBuilder);
    }
}
