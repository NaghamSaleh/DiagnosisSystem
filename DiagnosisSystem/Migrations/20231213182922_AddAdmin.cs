using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosisSystem.Migrations
{
    public partial class AddAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b8655327-f402-4b74-97de-470c0fbd7b2c", 0, "1ec35066-c205-4c66-8c51-7f6eb58b2410", "IdentityUser", "naghamsaleh@gmail.com", true, false, null, "NAGHAMSALEH@GMAIL.COM", "NAGHAMSALEH", "AQAAAAEAACcQAAAAEADYGTALc1htdn3ON0NZ0Wpy7s3k2Bu0URNbJG603tp5ov5y2MqzceZAUKCz13lQqw==", null, false, "01818502-19cb-4efe-91e3-0918d647b002", false, "naghamsaleh" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b8655327-f402-4b74-97de-470c0fbd7b2c");
        }
    }
}
