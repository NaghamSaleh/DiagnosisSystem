using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosisSystem.Migrations
{
    public partial class AddTagsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Specialities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialityNameSpecialtyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Specialities_SpecialityNameSpecialtyID",
                        column: x => x.SpecialityNameSpecialtyID,
                        principalTable: "Specialities",
                        principalColumn: "SpecialtyID",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");

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
                name: "Description",
                table: "Specialities");

            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

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
    }
}
