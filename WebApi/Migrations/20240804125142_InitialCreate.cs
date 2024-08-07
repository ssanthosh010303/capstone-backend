using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Designation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EscalationLevel = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designation", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GrievanceCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrievanceCategory", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GrievancePriority",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PriorityLevel = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrievancePriority", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GrievanceStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrievanceStatus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Otp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Expiry = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otp", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GrievanceSubcategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GrievanceCategoryId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrievanceSubcategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrievanceSubcategory_GrievanceCategory_GrievanceCategoryId",
                        column: x => x.GrievanceCategoryId,
                        principalTable: "GrievanceCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OpenGrievanceCount = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    DesignationId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Designation_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Grievance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Deadline = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Title = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAnonymous = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    GrievanceStatusId = table.Column<int>(type: "int", nullable: false),
                    GrievancePriorityId = table.Column<int>(type: "int", nullable: false),
                    GrievanceSubcategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    AssignedToId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grievance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grievance_Employee_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grievance_Employee_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grievance_GrievancePriority_GrievancePriorityId",
                        column: x => x.GrievancePriorityId,
                        principalTable: "GrievancePriority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grievance_GrievanceStatus_GrievanceStatusId",
                        column: x => x.GrievanceStatusId,
                        principalTable: "GrievanceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grievance_GrievanceSubcategory_GrievanceSubcategoryId",
                        column: x => x.GrievanceSubcategoryId,
                        principalTable: "GrievanceSubcategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FileAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BlobName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubmittedWithGrievanceId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileAttachment_Grievance_SubmittedWithGrievanceId",
                        column: x => x.SubmittedWithGrievanceId,
                        principalTable: "Grievance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GrievanceEmployee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    GrievanceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrievanceEmployee", x => new { x.EmployeeId, x.GrievanceId });
                    table.ForeignKey(
                        name: "FK_GrievanceEmployee_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrievanceEmployee_Grievance_GrievanceId",
                        column: x => x.GrievanceId,
                        principalTable: "Grievance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GrievanceResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GrievanceId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    MessageType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrievanceResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrievanceResponse_Employee_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrievanceResponse_Grievance_GrievanceId",
                        column: x => x.GrievanceId,
                        principalTable: "Grievance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Meeting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Duration = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    GrievanceId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    AttendeeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meeting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meeting_Employee_AttendeeId",
                        column: x => x.AttendeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meeting_Employee_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meeting_Grievance_GrievanceId",
                        column: x => x.GrievanceId,
                        principalTable: "Grievance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "CreatedOn", "IsActive", "Name", "UpdatedOn" },
                values: new object[] { 1, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(8692), true, "Application Management", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(8693) });

            migrationBuilder.InsertData(
                table: "Designation",
                columns: new[] { "Id", "CreatedOn", "EscalationLevel", "IsActive", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(8919), 100, true, "Employee", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(8920) },
                    { 2, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(8922), 200, true, "HR Manager", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(8922) },
                    { 3, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(8923), 300, true, "Senior HR Manager", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(8924) },
                    { 4, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(8925), 400, true, "Grievance Manager", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(8925) },
                    { 5, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(8926), 500, true, "Aduitor", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(8927) }
                });

            migrationBuilder.InsertData(
                table: "GrievanceCategory",
                columns: new[] { "Id", "CreatedOn", "Description", "IsActive", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9197), "Issues related to unwanted and inappropriate behavior or comments at work, including bullying and discrimination.", true, "Workplace Harassment", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9198) },
                    { 2, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9201), "Concerns about unequal or biased treatment in the workplace, including favoritism or discrimination.", true, "Unfair Treatment", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9201) },
                    { 3, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9202), "Problems related to the physical safety and well-being of employees, including unsafe working conditions or practices.", true, "Workplace Safety", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9202) },
                    { 4, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9203), "Challenges related to balancing work responsibilities with personal life, including excessive work hours or lack of flexibility.", true, "Work-life Balance", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9204) },
                    { 5, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9205), "Concerns regarding management practices, including poor leadership, lack of support, or ineffective communication.", true, "Managerial Issues", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9205) },
                    { 6, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9206), "Issues involving ethical conduct or integrity, such as conflicts of interest, fraud, or violations of company policies.", true, "Ethics and Integrity", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9206) },
                    { 7, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9207), "Problems related to company policies or administrative procedures, including inconsistent policy enforcement or bureaucratic hurdles.", true, "Administrative and Policy Issues", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9208) },
                    { 8, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9209), "Issues related to general conduct and behavior within the workplace, including breaches of company standards or norms.", true, "Workplace Conduct", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9209) },
                    { 9, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9210), "Concerns about pay, benefits, and other forms of compensation, including disputes over salary, bonuses, or benefits packages.", true, "Compensation and Benefits", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9210) }
                });

            migrationBuilder.InsertData(
                table: "GrievancePriority",
                columns: new[] { "Id", "CreatedOn", "IsActive", "Name", "PriorityLevel", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9111), true, "Low", 100, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9111) },
                    { 2, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9114), true, "Medium", 200, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9115) },
                    { 3, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9116), true, "High", 300, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9117) }
                });

            migrationBuilder.InsertData(
                table: "GrievanceStatus",
                columns: new[] { "Id", "CreatedOn", "IsActive", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9149), true, "Pending", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9150) },
                    { 2, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9152), true, "Approved", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9152) },
                    { 3, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9153), true, "Rejected", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9153) },
                    { 4, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9154), true, "Under Review", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9155) },
                    { 5, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9155), true, "Resolved", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9156) },
                    { 6, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9157), true, "Escalated", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9157) },
                    { 7, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9158), true, "Withdrawn", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9158) }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedOn", "IsActive", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(8966), true, "Admin", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(8966) },
                    { 2, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9062), true, "Manager", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9062) },
                    { 3, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9064), true, "User", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9064) },
                    { 4, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9065), true, "Microservice", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9065) }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "CreatedOn", "DepartmentId", "DesignationId", "Email", "FullName", "IsActive", "OpenGrievanceCount", "PasswordHash", "Phone", "RoleId", "UpdatedOn", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9484), 1, 1, "sakthisanthosh010303+admin@gmail.com", "Admin", true, 0, "AOh9uN10IO98hnsA7ah1q1pLCuHpkTnucOwnxYCDmTp7H3Hw", "+1 42547 45247", 1, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9485), "admin" },
                    { 2, new DateTime(2024, 8, 4, 12, 51, 41, 613, DateTimeKind.Utc).AddTicks(427), 1, 2, "sakthisanthosh010303+manager1@gmail.com", "Manager-1", true, 0, "aduKNy3Je1nlDpAChIE+nruYqGv/Ks8cVB1TqgfX1Qu19wgy", "+1 42547 45607", 2, new DateTime(2024, 8, 4, 12, 51, 41, 613, DateTimeKind.Utc).AddTicks(427), "manager1" },
                    { 3, new DateTime(2024, 8, 4, 12, 51, 41, 625, DateTimeKind.Utc).AddTicks(9828), 1, 2, "sakthisanthosh010303+manager2@gmail.com", "Manager-2", true, 0, "8HNo5z0HU7Y19CJcIQVKxroOjfpIp7GDBE/0kVK9ELrK3tdM", "+1 42547 45640", 2, new DateTime(2024, 8, 4, 12, 51, 41, 625, DateTimeKind.Utc).AddTicks(9829), "manager2" },
                    { 4, new DateTime(2024, 8, 4, 12, 51, 41, 639, DateTimeKind.Utc).AddTicks(69), 1, 3, "sakthisanthosh010303+manager3@gmail.com", "Manager-3", true, 0, "BL9z+Ti2gx7CXxzH7mgNDoT1HoYRjEQMviPA9AWM6d6KNCRR", "+1 42547 45647", 2, new DateTime(2024, 8, 4, 12, 51, 41, 639, DateTimeKind.Utc).AddTicks(69), "manager3" },
                    { 5, new DateTime(2024, 8, 4, 12, 51, 41, 652, DateTimeKind.Utc).AddTicks(1424), 1, 1, "sakthisanthosh010303@gmail.com", "Sakthi Santhosh", true, 0, "hq3GIJdhFcqDWnxS+5cilbl6qz3MHiTKi8kEehHkpD3/TGE7", "+1 42547 45248", 3, new DateTime(2024, 8, 4, 12, 51, 41, 652, DateTimeKind.Utc).AddTicks(1425), "sakthisanthosh" }
                });

            migrationBuilder.InsertData(
                table: "GrievanceSubcategory",
                columns: new[] { "Id", "CreatedOn", "GrievanceCategoryId", "IsActive", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9251), 1, true, "Sexual Harassment", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9251) },
                    { 2, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9256), 1, true, "Verbal Harassment", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9256) },
                    { 3, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9257), 1, true, "Physical Harassment", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9258) },
                    { 4, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9259), 1, true, "Psychological Harassment", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9259) },
                    { 5, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9260), 1, true, "Racial or Ethnic Harassment", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9260) },
                    { 6, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9261), 1, true, "Gender-based Harassment", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9261) },
                    { 7, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9262), 2, true, "Favoritism", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9263) },
                    { 8, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9264), 2, true, "Discrimination", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9264) },
                    { 9, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9265), 2, true, "Unequal Pay", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9265) },
                    { 10, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9266), 2, true, "Unequal Promotion Opportunities", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9267) },
                    { 11, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9268), 2, true, "Inconsistent Application of Rules", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9268) },
                    { 12, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9270), 2, true, "Lack of Recognition", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9270) },
                    { 13, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9271), 3, true, "Unsafe Equipment", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9272) },
                    { 14, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9273), 3, true, "Hazardous Work Conditions", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9273) },
                    { 15, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9275), 3, true, "Inadequate Safety Training", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9275) },
                    { 16, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9276), 3, true, "Lack of Personal Protective Equipment (PPE)", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9276) },
                    { 17, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9277), 3, true, "Emergency Preparedness", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9278) },
                    { 18, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9279), 3, true, "Ergonomic Issues", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9279) },
                    { 19, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9280), 4, true, "Excessive Work Hours", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9280) },
                    { 20, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9281), 4, true, "Inflexible Work Schedules", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9282) },
                    { 21, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9283), 4, true, "Lack of Paid Time Off", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9284) },
                    { 22, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9285), 4, true, "Remote Work Limitations", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9285) },
                    { 23, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9287), 4, true, "Difficulty in Taking Breaks", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9287) },
                    { 24, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9288), 4, true, "Job Stress", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9288) },
                    { 25, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9289), 5, true, "Poor Communication", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9290) },
                    { 26, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9292), 5, true, "Lack of Support", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9292) },
                    { 27, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9294), 5, true, "Micromanagement", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9294) },
                    { 28, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9295), 5, true, "Inconsistent Feedback", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9295) },
                    { 29, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9296), 5, true, "Unclear Expectations", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9297) },
                    { 30, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9298), 5, true, "Favoritism in Decision-Making", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9299) },
                    { 31, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9300), 6, true, "Conflict of Interest", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9300) },
                    { 32, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9301), 6, true, "Fraudulent Activities", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9301) },
                    { 33, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9302), 6, true, "Breach of Confidentiality", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9303) },
                    { 34, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9304), 6, true, "Violations of Company Policies", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9304) },
                    { 35, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9305), 6, true, "Corruption", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9305) },
                    { 36, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9306), 6, true, "Misrepresentation", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9306) },
                    { 37, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9307), 7, true, "Inconsistent Policy Enforcement", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9308) },
                    { 38, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9309), 7, true, "Outdated Policies", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9309) },
                    { 39, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9310), 7, true, "Ineffective Procedures", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9310) },
                    { 40, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9311), 7, true, "Bureaucratic Delays", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9312) },
                    { 41, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9313), 7, true, "Lack of Policy Transparency", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9313) },
                    { 42, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9314), 7, true, "Administrative Errors", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9314) },
                    { 43, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9315), 8, true, "Unprofessional Behavior", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9316) },
                    { 44, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9316), 8, true, "Attendance Issues", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9317) },
                    { 45, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9318), 8, true, "Dress Code Violations", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9318) },
                    { 46, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9319), 8, true, "Disruptive Behavior", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9319) },
                    { 47, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9320), 8, true, "Misuse of Company Resources", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9321) },
                    { 48, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9321), 8, true, "Breaches of Company Standards", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9322) },
                    { 49, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9323), 9, true, "Salary Discrepancies", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9323) },
                    { 50, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9324), 9, true, "Bonus Disputes", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9324) },
                    { 51, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9325), 9, true, "Benefits Enrollment Issues", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9326) },
                    { 52, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9327), 9, true, "Health Insurance Problems", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9327) },
                    { 53, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9328), 9, true, "Retirement Plan Concerns", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9328) },
                    { 54, new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9329), 9, true, "Expense Reimbursement Issues", new DateTime(2024, 8, 4, 12, 51, 41, 599, DateTimeKind.Utc).AddTicks(9330) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Department_Name",
                table: "Department",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Designation_EscalationLevel",
                table: "Designation",
                column: "EscalationLevel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Designation_Name",
                table: "Designation",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DesignationId",
                table: "Employee",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Email",
                table: "Employee",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Phone",
                table: "Employee",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_RoleId",
                table: "Employee",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Username",
                table: "Employee",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachment_BlobName",
                table: "FileAttachment",
                column: "BlobName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachment_SubmittedWithGrievanceId",
                table: "FileAttachment",
                column: "SubmittedWithGrievanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Grievance_AssignedToId",
                table: "Grievance",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Grievance_CreatedById",
                table: "Grievance",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Grievance_GrievancePriorityId",
                table: "Grievance",
                column: "GrievancePriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Grievance_GrievanceStatusId",
                table: "Grievance",
                column: "GrievanceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Grievance_GrievanceSubcategoryId",
                table: "Grievance",
                column: "GrievanceSubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GrievanceEmployee_GrievanceId",
                table: "GrievanceEmployee",
                column: "GrievanceId");

            migrationBuilder.CreateIndex(
                name: "IX_GrievancePriority_Name",
                table: "GrievancePriority",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GrievancePriority_PriorityLevel",
                table: "GrievancePriority",
                column: "PriorityLevel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GrievanceResponse_CreatedById",
                table: "GrievanceResponse",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_GrievanceResponse_GrievanceId",
                table: "GrievanceResponse",
                column: "GrievanceId");

            migrationBuilder.CreateIndex(
                name: "IX_GrievanceStatus_Name",
                table: "GrievanceStatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GrievanceSubcategory_GrievanceCategoryId",
                table: "GrievanceSubcategory",
                column: "GrievanceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GrievanceSubcategory_Name",
                table: "GrievanceSubcategory",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_AttendeeId",
                table: "Meeting",
                column: "AttendeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_CreatedById",
                table: "Meeting",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_GrievanceId",
                table: "Meeting",
                column: "GrievanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Role",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileAttachment");

            migrationBuilder.DropTable(
                name: "GrievanceEmployee");

            migrationBuilder.DropTable(
                name: "GrievanceResponse");

            migrationBuilder.DropTable(
                name: "Meeting");

            migrationBuilder.DropTable(
                name: "Otp");

            migrationBuilder.DropTable(
                name: "Grievance");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "GrievancePriority");

            migrationBuilder.DropTable(
                name: "GrievanceStatus");

            migrationBuilder.DropTable(
                name: "GrievanceSubcategory");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Designation");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "GrievanceCategory");
        }
    }
}
