using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosisSystem.Migrations
{
    public partial class AddTransactDoctorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f923a35-b8d6-4fa7-a69d-ce1aadbbe24f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "238dc023-afa6-4f1b-b298-6f13e31e91f2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e443af2-2b85-4d51-b34e-2f9cc6bcab0f");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6609a15d-7ba4-4db7-a6e1-f413aca6d96d", "0b04c1d1-a51d-4c5d-8768-d5c474433e5d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6609a15d-7ba4-4db7-a6e1-f413aca6d96d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b04c1d1-a51d-4c5d-8768-d5c474433e5d");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "transactDoctors",
                columns: table => new
                {
                    GuidId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Languages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentHospital = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortBio = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactDoctors", x => x.GuidId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "17664d6f-b8e9-42d1-b4d4-f44875837fe2", "3952ff9c-b7ec-4deb-adc1-4beb3086fadd", "Doctor", "DOCTOR" },
                    { "81d9378c-9692-4ac4-9053-ef1a6a2ba2da", "767b14cd-429f-489e-ab72-b31d8423d90f", "Patient", "PATIENT" },
                    { "9d1184f0-05d6-4af6-b55a-2ad64d9b4859", "75ba4ba9-302d-42d0-8d49-bd9500a458ec", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "50c05c19-c83b-42cf-a469-6e60beb02cb4", 0, "e81ada08-aef6-4fd5-9cc5-c588a26fb603", "IdentityUser", "naghamsaleh@gmail.com", true, false, null, "NAGHAMSALEH@GMAIL.COM", "NAGHAMSALEH", "AQAAAAEAACcQAAAAEHVEqyoRATaQHxsvV+DtRlx3WR4wqxK0wG3jUIM5i80dFXZtZRAaSq3XIZJBJI/7hQ==", null, false, "67b8b2d9-bbb0-4395-b574-b637bfaa13cb", false, "naghamsaleh" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9d1184f0-05d6-4af6-b55a-2ad64d9b4859", "50c05c19-c83b-42cf-a469-6e60beb02cb4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactDoctors");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17664d6f-b8e9-42d1-b4d4-f44875837fe2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81d9378c-9692-4ac4-9053-ef1a6a2ba2da");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9d1184f0-05d6-4af6-b55a-2ad64d9b4859", "50c05c19-c83b-42cf-a469-6e60beb02cb4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d1184f0-05d6-4af6-b55a-2ad64d9b4859");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "50c05c19-c83b-42cf-a469-6e60beb02cb4");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f923a35-b8d6-4fa7-a69d-ce1aadbbe24f", "5d108870-694a-47ac-a22c-c858d20b7444", "InitialDoctor", "INITIALDOCTOR" },
                    { "238dc023-afa6-4f1b-b298-6f13e31e91f2", "e7ffd2a2-7a72-4a80-b409-b12c162474b4", "Doctor", "DOCTOR" },
                    { "5e443af2-2b85-4d51-b34e-2f9cc6bcab0f", "0995248f-0b90-4178-98c6-0a5e34a2b13b", "Patient", "PATIENT" },
                    { "6609a15d-7ba4-4db7-a6e1-f413aca6d96d", "8f10ad6c-81c6-4934-b132-6da8a70ef480", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0b04c1d1-a51d-4c5d-8768-d5c474433e5d", 0, "f0520457-b894-46e7-a15a-9c8cef52a1ca", "IdentityUser", "naghamsaleh@gmail.com", true, false, null, "NAGHAMSALEH@GMAIL.COM", "NAGHAMSALEH", "AQAAAAEAACcQAAAAEBxLWuJn5EUbsfx0OhRXwAuNrqQyDV8TWuqh3BhSqiyp5qyqZSaOZY9cg11eWsokJg==", null, false, "01713829-7aa4-4a3f-bc6d-a97a6e037944", false, "naghamsaleh" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6609a15d-7ba4-4db7-a6e1-f413aca6d96d", "0b04c1d1-a51d-4c5d-8768-d5c474433e5d" });
        }
    }
}
