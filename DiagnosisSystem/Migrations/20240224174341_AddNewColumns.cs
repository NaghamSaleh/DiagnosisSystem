using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosisSystem.Migrations
{
    public partial class AddNewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
           
            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "Queries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "paidConstultant",
                table: "Queries",
                type: "bit",
                nullable: false,
                defaultValue: false);

           

            migrationBuilder.CreateIndex(
                name: "IX_Queries_DoctorId",
                table: "Queries",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Queries_AspNetUsers_DoctorId",
                table: "Queries",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Queries_AspNetUsers_DoctorId",
                table: "Queries");

            migrationBuilder.DropIndex(
                name: "IX_Queries_DoctorId",
                table: "Queries");

        

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Queries");

            migrationBuilder.DropColumn(
                name: "paidConstultant",
                table: "Queries");

          
          
        }
    }
}
