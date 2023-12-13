using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosisSystem.Migrations
{
    public partial class DeleteTransactDoctorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28e2d759-b1c9-4714-8945-f480a474215c", "aaa04d4f-5e58-43ea-a0b0-06c917deb23f", "Doctor", "DOCTOR" },
                    { "3300251b-750d-45ad-ad25-6b9f6143e1a4", "ea367ed6-034f-4c59-9cb0-7ff33f3099e9", "InitialDoctor", "INITIALDOCTOR" },
                    { "cc36eefb-58e2-429f-8aa9-39dd224aec58", "5623d0e9-b304-4044-bbd8-3d2fb58cd74d", "Admin", "ADMIN" },
                    { "cd053651-7e7f-4115-a9fa-269ede36eaf6", "572b72b5-b1af-463c-a544-b08b03adbdd4", "Patient", "PATIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e1012af4-8c03-4814-b740-4142c6d3a520", 0, "99e99b9f-12f3-463d-8950-608d6f257b24", "IdentityUser", "naghamsaleh@gmail.com", true, false, null, "NAGHAMSALEH@GMAIL.COM", "NAGHAMSALEH", "AQAAAAEAACcQAAAAEMjsEiKp7VOa92ROVynqahX4xuT16NICl3pgyuqRuO4k9G34MLF9WKI5z3ltPwzS1w==", null, false, "5875aa00-358f-4f40-b53d-72412db2868c", false, "naghamsaleh" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cc36eefb-58e2-429f-8aa9-39dd224aec58", "e1012af4-8c03-4814-b740-4142c6d3a520" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28e2d759-b1c9-4714-8945-f480a474215c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3300251b-750d-45ad-ad25-6b9f6143e1a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd053651-7e7f-4115-a9fa-269ede36eaf6");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cc36eefb-58e2-429f-8aa9-39dd224aec58", "e1012af4-8c03-4814-b740-4142c6d3a520" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc36eefb-58e2-429f-8aa9-39dd224aec58");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1012af4-8c03-4814-b740-4142c6d3a520");

            migrationBuilder.CreateTable(
                name: "transactDoctors",
                columns: table => new
                {
                    GuidId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentHospital = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Languages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortBio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
    }
}
