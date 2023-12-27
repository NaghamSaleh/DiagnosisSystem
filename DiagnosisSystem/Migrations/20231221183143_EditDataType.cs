using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosisSystem.Migrations
{
    public partial class EditDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientQuestions_Tags_QuestionTagId",
                table: "PatientQuestions");

            migrationBuilder.DropIndex(
                name: "IX_PatientQuestions_QuestionTagId",
                table: "PatientQuestions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b3e8f5e-705b-4bd9-a2ea-f1a62834b5e4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2fe5c4f-7aab-4b5e-a6e0-1f748898cc02");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f082ac4e-cacc-4e28-9e65-8aacdee97123");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "13967c44-80c1-4e5b-98c2-22d78a061235", "8674cc54-c9e4-421d-b48a-da0f1754fd66" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13967c44-80c1-4e5b-98c2-22d78a061235");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8674cc54-c9e4-421d-b48a-da0f1754fd66");

            migrationBuilder.DropColumn(
                name: "QuestionTagId",
                table: "PatientQuestions");

            migrationBuilder.AddColumn<string>(
                name: "QuestionTag",
                table: "PatientQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SpecialityVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "QuestionTag",
                table: "PatientQuestions");

            migrationBuilder.AddColumn<int>(
                name: "QuestionTagId",
                table: "PatientQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13967c44-80c1-4e5b-98c2-22d78a061235", "f411946c-a3e6-4a72-b36b-2b45d5624542", "Admin", "ADMIN" },
                    { "1b3e8f5e-705b-4bd9-a2ea-f1a62834b5e4", "5a414a9e-4583-4112-8a3f-0b93c7ff8f61", "Doctor", "DOCTOR" },
                    { "a2fe5c4f-7aab-4b5e-a6e0-1f748898cc02", "7a3706be-e130-4178-b059-968c3b3b6bef", "InitialDoctor", "INITIALDOCTOR" },
                    { "f082ac4e-cacc-4e28-9e65-8aacdee97123", "df7cc76a-953d-45b4-80eb-660d748ab2e6", "Patient", "PATIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8674cc54-c9e4-421d-b48a-da0f1754fd66", 0, "0b02ddc4-df6d-4b71-b950-a230ed7681a0", "IdentityUser", "naghamsaleh@gmail.com", true, false, null, "NAGHAMSALEH@GMAIL.COM", "NAGHAMSALEH", "AQAAAAEAACcQAAAAEKnFXdk4t8JrsFTwizR2W4T2l0K6hlO8VlM5uaT0xBEpJz2ML7aAdujIrwr89F2oQw==", null, false, "61e82759-38ce-4d8e-a959-7dacde092ab1", false, "naghamsaleh" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "13967c44-80c1-4e5b-98c2-22d78a061235", "8674cc54-c9e4-421d-b48a-da0f1754fd66" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientQuestions_QuestionTagId",
                table: "PatientQuestions",
                column: "QuestionTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientQuestions_Tags_QuestionTagId",
                table: "PatientQuestions",
                column: "QuestionTagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
