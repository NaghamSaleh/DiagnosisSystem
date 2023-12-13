using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosisSystem.Migrations
{
    public partial class AddRolesandAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b8655327-f402-4b74-97de-470c0fbd7b2c");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b8655327-f402-4b74-97de-470c0fbd7b2c", 0, "1ec35066-c205-4c66-8c51-7f6eb58b2410", "IdentityUser", "naghamsaleh@gmail.com", true, false, null, "NAGHAMSALEH@GMAIL.COM", "NAGHAMSALEH", "AQAAAAEAACcQAAAAEADYGTALc1htdn3ON0NZ0Wpy7s3k2Bu0URNbJG603tp5ov5y2MqzceZAUKCz13lQqw==", null, false, "01818502-19cb-4efe-91e3-0918d647b002", false, "naghamsaleh" });
        }
    }
}
