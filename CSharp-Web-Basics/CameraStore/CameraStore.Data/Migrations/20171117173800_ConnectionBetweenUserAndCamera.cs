using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CameraStore.Data.Migrations
{
    public partial class ConnectionBetweenUserAndCamera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Cameras",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_UserId",
                table: "Cameras",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cameras_AspNetUsers_UserId",
                table: "Cameras",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cameras_AspNetUsers_UserId",
                table: "Cameras");

            migrationBuilder.DropIndex(
                name: "IX_Cameras_UserId",
                table: "Cameras");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cameras");
        }
    }
}
