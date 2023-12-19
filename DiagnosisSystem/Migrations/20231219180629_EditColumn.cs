using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosisSystem.Migrations
{
    public partial class EditColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Specialities_SpecialityNameSpecialtyID",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_SpecialityNameSpecialtyID",
                table: "Tags");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26a12539-e881-47bc-8445-e05d1bde14bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d781003-2710-4a4e-bd40-46be42666bcc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "740647cf-ec3e-424e-953c-54527e2225d3");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "62ae42d7-92c1-4188-abf9-5d1c720743bd", "70d8b305-bd45-4bff-b0bd-842970562db1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62ae42d7-92c1-4188-abf9-5d1c720743bd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "70d8b305-bd45-4bff-b0bd-842970562db1");

            migrationBuilder.DropColumn(
                name: "SpecialityNameSpecialtyID",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "SpecialityName",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PatientQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientQuestions_Tags_QuestionTagId",
                        column: x => x.QuestionTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientQuestions");

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
                name: "SpecialityName",
                table: "Tags");

            migrationBuilder.AddColumn<int>(
                name: "SpecialityNameSpecialtyID",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26a12539-e881-47bc-8445-e05d1bde14bb", "671ecc18-4839-4b32-8358-e17ca80f3d26", "Patient", "PATIENT" },
                    { "4d781003-2710-4a4e-bd40-46be42666bcc", "09453b7a-5f0a-478f-bf10-9456aeaffeb7", "Doctor", "DOCTOR" },
                    { "62ae42d7-92c1-4188-abf9-5d1c720743bd", "3046226b-b0d7-4754-8641-505f4085c8ca", "Admin", "ADMIN" },
                    { "740647cf-ec3e-424e-953c-54527e2225d3", "8765093c-435f-4d4b-a85d-4fd116909261", "InitialDoctor", "INITIALDOCTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "70d8b305-bd45-4bff-b0bd-842970562db1", 0, "cd03786d-eb37-4d26-a2dc-b59d7fa2c73b", "IdentityUser", "naghamsaleh@gmail.com", true, false, null, "NAGHAMSALEH@GMAIL.COM", "NAGHAMSALEH", "AQAAAAEAACcQAAAAEMZ/Nulr0zJkZbYP7MC57qb7hRVFRKvudmsq05XoCeOc9K6iaf5j7xmU+UIqQM7RRQ==", null, false, "7fdc34ad-f107-43d8-be25-7e591b10242a", false, "naghamsaleh" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "62ae42d7-92c1-4188-abf9-5d1c720743bd", "70d8b305-bd45-4bff-b0bd-842970562db1" });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_SpecialityNameSpecialtyID",
                table: "Tags",
                column: "SpecialityNameSpecialtyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Specialities_SpecialityNameSpecialtyID",
                table: "Tags",
                column: "SpecialityNameSpecialtyID",
                principalTable: "Specialities",
                principalColumn: "SpecialtyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
