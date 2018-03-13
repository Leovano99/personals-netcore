using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class addForeignKey_MSProjectInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "projectCode",
                table: "MS_ProjectInfo");

            migrationBuilder.AddColumn<int>(
                name: "projectID",
                table: "MS_ProjectInfo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MS_ProjectInfo_projectID",
                table: "MS_ProjectInfo",
                column: "projectID");

            migrationBuilder.AddForeignKey(
                name: "FK_MS_ProjectInfo_MS_Project_projectID",
                table: "MS_ProjectInfo",
                column: "projectID",
                principalTable: "MS_Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_ProjectInfo_MS_Project_projectID",
                table: "MS_ProjectInfo");

            migrationBuilder.DropIndex(
                name: "IX_MS_ProjectInfo_projectID",
                table: "MS_ProjectInfo");

            migrationBuilder.DropColumn(
                name: "projectID",
                table: "MS_ProjectInfo");

            migrationBuilder.AddColumn<string>(
                name: "projectCode",
                table: "MS_ProjectInfo",
                maxLength: 5,
                nullable: false,
                defaultValue: "");
        }
    }
}
