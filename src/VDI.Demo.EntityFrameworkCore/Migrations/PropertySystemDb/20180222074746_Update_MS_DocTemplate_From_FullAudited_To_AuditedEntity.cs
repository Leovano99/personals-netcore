using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class Update_MS_DocTemplate_From_FullAudited_To_AuditedEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "MS_DocTemplate");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "MS_DocTemplate");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MS_DocTemplate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "MS_DocTemplate",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "MS_DocTemplate",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MS_DocTemplate",
                nullable: false,
                defaultValue: false);
        }
    }
}
