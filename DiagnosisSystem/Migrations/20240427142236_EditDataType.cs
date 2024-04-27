using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosisSystem.Migrations
{
    public partial class EditDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DiscussionForums_DiscussionForumId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionForums_AspNetUsers_GroupAdminId",
                table: "DiscussionForums");

            migrationBuilder.DropIndex(
                name: "IX_DiscussionForums_GroupAdminId",
                table: "DiscussionForums");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DiscussionForumId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GroupAdminId",
                table: "DiscussionForums");

            migrationBuilder.DropColumn(
                name: "DiscussionForumId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "GroupAdmin",
                table: "DiscussionForums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SelectedMembers",
                table: "DiscussionForums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

  
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "GroupAdmin",
                table: "DiscussionForums");

            migrationBuilder.DropColumn(
                name: "SelectedMembers",
                table: "DiscussionForums");

            migrationBuilder.AddColumn<string>(
                name: "GroupAdminId",
                table: "DiscussionForums",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DiscussionForumId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

     

          
            migrationBuilder.CreateIndex(
                name: "IX_DiscussionForums_GroupAdminId",
                table: "DiscussionForums",
                column: "GroupAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DiscussionForumId",
                table: "AspNetUsers",
                column: "DiscussionForumId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DiscussionForums_DiscussionForumId",
                table: "AspNetUsers",
                column: "DiscussionForumId",
                principalTable: "DiscussionForums",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionForums_AspNetUsers_GroupAdminId",
                table: "DiscussionForums",
                column: "GroupAdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
