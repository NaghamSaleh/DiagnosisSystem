using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosisSystem.Migrations
{
    public partial class RemoveTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecialityVM");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d1f4fa0-9ed6-4c9c-be7b-8c102c4532b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c6ca586-c656-44bc-be57-223ffbcffa54");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef5a3135-21b9-4b22-85d8-db9dde144860");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b16c91ab-fe53-4aee-bdc0-145fae4c2419", "453b435e-3d73-449c-a292-987567ac5516" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b16c91ab-fe53-4aee-bdc0-145fae4c2419");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "453b435e-3d73-449c-a292-987567ac5516");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12e30e74-c327-43f0-81ec-521f44053373", "2dafe310-b53b-4872-a110-fc8ddbb0017b", "Patient", "PATIENT" },
                    { "1407683f-37a4-487f-ae8b-6d6d799f98d5", "89a752ad-9cd7-4d3a-ac04-88b6ed6e9049", "Admin", "ADMIN" },
                    { "61649609-eb79-4f74-9faf-70d25af80444", "523d8373-b528-4ad1-80bd-1b65c788ccc6", "InitialDoctor", "INITIALDOCTOR" },
                    { "79266339-576a-4372-8d9f-2d3b7c0bf2f8", "05eadd7b-c822-4333-b955-d90e98b401ba", "Doctor", "DOCTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "542cadcb-d0de-4827-8ccd-a3289650363a", 0, "5bce0b5d-e841-42dd-8fb3-0a69e7935443", "IdentityUser", "naghamsaleh@gmail.com", true, false, null, "NAGHAMSALEH@GMAIL.COM", "NAGHAMSALEH", "AQAAAAEAACcQAAAAEM1sVJoOJYwMOH8ny/kcv+IEyryBl1/KRoSxEXlmWKim9xjXPO3cnUkEG0f/fGbHUg==", null, false, "9e727725-9e41-4ca8-8276-63004a8f7b5b", false, "naghamsaleh" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1407683f-37a4-487f-ae8b-6d6d799f98d5", "542cadcb-d0de-4827-8ccd-a3289650363a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12e30e74-c327-43f0-81ec-521f44053373");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61649609-eb79-4f74-9faf-70d25af80444");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79266339-576a-4372-8d9f-2d3b7c0bf2f8");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1407683f-37a4-487f-ae8b-6d6d799f98d5", "542cadcb-d0de-4827-8ccd-a3289650363a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1407683f-37a4-487f-ae8b-6d6d799f98d5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "542cadcb-d0de-4827-8ccd-a3289650363a");

            migrationBuilder.CreateTable(
                name: "SpecialityVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialityVM", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d1f4fa0-9ed6-4c9c-be7b-8c102c4532b9", "2b7aee9b-08e7-45a7-bb4b-0e1d779889c1", "Doctor", "DOCTOR" },
                    { "2c6ca586-c656-44bc-be57-223ffbcffa54", "bd76e30a-90f5-4f6a-9115-79e2e7caeede", "InitialDoctor", "INITIALDOCTOR" },
                    { "b16c91ab-fe53-4aee-bdc0-145fae4c2419", "4f4a4d45-3862-4e39-aaef-8cda6069b8e6", "Admin", "ADMIN" },
                    { "ef5a3135-21b9-4b22-85d8-db9dde144860", "c0980ce6-74b9-4c04-9c73-a07aaba1d979", "Patient", "PATIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "453b435e-3d73-449c-a292-987567ac5516", 0, "4adda30c-53f7-4d0f-8df6-85e399ad3726", "IdentityUser", "naghamsaleh@gmail.com", true, false, null, "NAGHAMSALEH@GMAIL.COM", "NAGHAMSALEH", "AQAAAAEAACcQAAAAEIIO+wgKFTHCCPe8NQwZ2QhHJzj2HZQd1ovBYzOiA6HGu8zhTGjXQapxUYwCLD9AlQ==", null, false, "89976c42-e285-4c8a-8a14-e35cb907629b", false, "naghamsaleh" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b16c91ab-fe53-4aee-bdc0-145fae4c2419", "453b435e-3d73-449c-a292-987567ac5516" });
        }
    }
}
