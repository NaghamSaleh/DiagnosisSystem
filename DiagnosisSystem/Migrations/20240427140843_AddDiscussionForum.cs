using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosisSystem.Migrations
{
    public partial class AddDiscussionForum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Queries_AspNetUsers_DoctorId",
                table: "Queries");

            migrationBuilder.DropIndex(
                name: "IX_Queries_DoctorId",
                table: "Queries");

            

          
        

            migrationBuilder.RenameColumn(
                name: "paidConstultant",
                table: "Queries",
                newName: "PaidConstultant");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Queries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DiscussionForumId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DiscussionForums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscussionTopic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupAdminId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionForums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscussionForums_AspNetUsers_GroupAdminId",
                        column: x => x.GroupAdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

         
          
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DiscussionForumId",
                table: "AspNetUsers",
                column: "DiscussionForumId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionForums_GroupAdminId",
                table: "DiscussionForums",
                column: "GroupAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DiscussionForums_DiscussionForumId",
                table: "AspNetUsers",
                column: "DiscussionForumId",
                principalTable: "DiscussionForums",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DiscussionForums_DiscussionForumId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DiscussionForums");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DiscussionForumId",
                table: "AspNetUsers");


            migrationBuilder.DropColumn(
                name: "DiscussionForumId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PaidConstultant",
                table: "Queries",
                newName: "paidConstultant");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Queries",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

          

         

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
    }
}
