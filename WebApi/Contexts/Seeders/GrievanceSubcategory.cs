using Microsoft.EntityFrameworkCore;

using WebApi.Models;

namespace WebApi.Contexts.Seeders
{
    public static class GrievanceSubcategorySeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<GrievanceSubcategoryModel>().HasData(
                new GrievanceSubcategoryModel
                {
                    Id = 1,
                    GrievanceCategoryId = 1,
                    Name = "Sexual Harassment"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 2,
                    GrievanceCategoryId = 1,
                    Name = "Verbal Harassment"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 3,
                    GrievanceCategoryId = 1,
                    Name = "Physical Harassment"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 4,
                    GrievanceCategoryId = 1,
                    Name = "Psychological Harassment"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 5,
                    GrievanceCategoryId = 1,
                    Name = "Racial or Ethnic Harassment"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 6,
                    GrievanceCategoryId = 1,
                    Name = "Gender-based Harassment"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 7,
                    GrievanceCategoryId = 2,
                    Name = "Favoritism"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 8,
                    GrievanceCategoryId = 2,
                    Name = "Discrimination"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 9,
                    GrievanceCategoryId = 2,
                    Name = "Unequal Pay"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 10,
                    GrievanceCategoryId = 2,
                    Name = "Unequal Promotion Opportunities"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 11,
                    GrievanceCategoryId = 2,
                    Name = "Inconsistent Application of Rules"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 12,
                    GrievanceCategoryId = 2,
                    Name = "Lack of Recognition"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 13,
                    GrievanceCategoryId = 3,
                    Name = "Unsafe Equipment"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 14,
                    GrievanceCategoryId = 3,
                    Name = "Hazardous Work Conditions"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 15,
                    GrievanceCategoryId = 3,
                    Name = "Inadequate Safety Training"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 16,
                    GrievanceCategoryId = 3,
                    Name = "Lack of Personal Protective Equipment (PPE)"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 17,
                    GrievanceCategoryId = 3,
                    Name = "Emergency Preparedness"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 18,
                    GrievanceCategoryId = 3,
                    Name = "Ergonomic Issues"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 19,
                    GrievanceCategoryId = 4,
                    Name = "Excessive Work Hours"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 20,
                    GrievanceCategoryId = 4,
                    Name = "Inflexible Work Schedules"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 21,
                    GrievanceCategoryId = 4,
                    Name = "Lack of Paid Time Off"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 22,
                    GrievanceCategoryId = 4,
                    Name = "Remote Work Limitations"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 23,
                    GrievanceCategoryId = 4,
                    Name = "Difficulty in Taking Breaks"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 24,
                    GrievanceCategoryId = 4,
                    Name = "Job Stress"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 25,
                    GrievanceCategoryId = 5,
                    Name = "Poor Communication"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 26,
                    GrievanceCategoryId = 5,
                    Name = "Lack of Support"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 27,
                    GrievanceCategoryId = 5,
                    Name = "Micromanagement"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 28,
                    GrievanceCategoryId = 5,
                    Name = "Inconsistent Feedback"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 29,
                    GrievanceCategoryId = 5,
                    Name = "Unclear Expectations"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 30,
                    GrievanceCategoryId = 5,
                    Name = "Favoritism in Decision-Making"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 31,
                    GrievanceCategoryId = 6,
                    Name = "Conflict of Interest"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 32,
                    GrievanceCategoryId = 6,
                    Name = "Fraudulent Activities"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 33,
                    GrievanceCategoryId = 6,
                    Name = "Breach of Confidentiality"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 34,
                    GrievanceCategoryId = 6,
                    Name = "Violations of Company Policies"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 35,
                    GrievanceCategoryId = 6,
                    Name = "Corruption"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 36,
                    GrievanceCategoryId = 6,
                    Name = "Misrepresentation"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 37,
                    GrievanceCategoryId = 7,
                    Name = "Inconsistent Policy Enforcement"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 38,
                    GrievanceCategoryId = 7,
                    Name = "Outdated Policies"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 39,
                    GrievanceCategoryId = 7,
                    Name = "Ineffective Procedures"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 40,
                    GrievanceCategoryId = 7,
                    Name = "Bureaucratic Delays"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 41,
                    GrievanceCategoryId = 7,
                    Name = "Lack of Policy Transparency"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 42,
                    GrievanceCategoryId = 7,
                    Name = "Administrative Errors"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 43,
                    GrievanceCategoryId = 8,
                    Name = "Unprofessional Behavior"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 44,
                    GrievanceCategoryId = 8,
                    Name = "Attendance Issues"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 45,
                    GrievanceCategoryId = 8,
                    Name = "Dress Code Violations"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 46,
                    GrievanceCategoryId = 8,
                    Name = "Disruptive Behavior"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 47,
                    GrievanceCategoryId = 8,
                    Name = "Misuse of Company Resources"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 48,
                    GrievanceCategoryId = 8,
                    Name = "Breaches of Company Standards"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 49,
                    GrievanceCategoryId = 9,
                    Name = "Salary Discrepancies"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 50,
                    GrievanceCategoryId = 9,
                    Name = "Bonus Disputes"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 51,
                    GrievanceCategoryId = 9,
                    Name = "Benefits Enrollment Issues"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 52,
                    GrievanceCategoryId = 9,
                    Name = "Health Insurance Problems"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 53,
                    GrievanceCategoryId = 9,
                    Name = "Retirement Plan Concerns"
                },
                new GrievanceSubcategoryModel
                {
                    Id = 54,
                    GrievanceCategoryId = 9,
                    Name = "Expense Reimbursement Issues"
                }
            );
        }
    }
}
