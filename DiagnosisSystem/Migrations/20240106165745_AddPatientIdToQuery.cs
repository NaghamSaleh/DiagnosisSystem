using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosisSystem.Migrations
{
    public partial class AddPatientIdToQuery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                table: "PatientQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "PatientQuestions");

          
        }
    }
}
