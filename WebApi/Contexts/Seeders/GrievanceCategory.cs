using Microsoft.EntityFrameworkCore;

using WebApi.Models;

namespace WebApi.Contexts.Seeders;

public static class GrievanceCategorySeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<GrievanceCategoryModel>().HasData(
            new GrievanceCategoryModel
            {
                Id = 1,
                Name = "Workplace Harassment",
                Description = "Issues related to unwanted and inappropriate "
                    + "behavior or comments at work, including bullying and "
                    + "discrimination."
            },
            new GrievanceCategoryModel
            {
                Id = 2,
                Name = "Unfair Treatment",
                Description = "Concerns about unequal or biased treatment in "
                    + "the workplace, including favoritism or discrimination."
            },
            new GrievanceCategoryModel
            {
                Id = 3,
                Name = "Workplace Safety",
                Description = "Problems related to the physical safety and "
                    + "well-being of employees, including unsafe working "
                    + "conditions or practices."
            },
            new GrievanceCategoryModel
            {
                Id = 4,
                Name = "Work-life Balance",
                Description = "Challenges related to balancing work "
                    + "responsibilities with personal life, including "
                    + "excessive work hours or lack of flexibility."
            },
            new GrievanceCategoryModel
            {
                Id = 5,
                Name = "Managerial Issues",
                Description = "Concerns regarding management practices, "
                    + "including poor leadership, lack of support, or "
                    + "ineffective communication."
            },
            new GrievanceCategoryModel
            {
                Id = 6,
                Name = "Ethics and Integrity",
                Description = "Issues involving ethical conduct or integrity, "
                    + "such as conflicts of interest, fraud, or violations of "
                    + "company policies."
            },
            new GrievanceCategoryModel
            {
                Id = 7,
                Name = "Administrative and Policy Issues",
                Description = "Problems related to company policies or "
                    + "administrative procedures, including inconsistent "
                    + "policy enforcement or bureaucratic hurdles."
            },
            new GrievanceCategoryModel
            {
                Id = 8,
                Name = "Workplace Conduct",
                Description = "Issues related to general conduct and behavior "
                    + "within the workplace, including breaches of company "
                    + "standards or norms."
            },
            new GrievanceCategoryModel
            {
                Id = 9,
                Name = "Compensation and Benefits",
                Description = "Concerns about pay, benefits, and other forms "
                + "of compensation, including disputes over salary, bonuses, "
                + "or benefits packages."
            }
        );
    }
}
