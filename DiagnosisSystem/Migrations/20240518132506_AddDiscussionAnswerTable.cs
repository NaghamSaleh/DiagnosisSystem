using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosisSystem.Migrations
{
    public partial class AddDiscussionAnswerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "DiscussionAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ForumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnsweredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscussionAnswers_DiscussionForums_ForumId",
                        column: x => x.ForumId,
                        principalTable: "DiscussionForums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

          

           
            migrationBuilder.CreateIndex(
                name: "IX_DiscussionAnswers_ForumId",
                table: "DiscussionAnswers",
                column: "ForumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscussionAnswers");

    
        }
    }
}
