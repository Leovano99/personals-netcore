using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class Add_Field_Detail_andProjectInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "projectImageLogo",
                table: "MS_ProjectInfo",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "detailImage",
                table: "MS_Detail",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "projectImageLogo",
                table: "MS_ProjectInfo");

            migrationBuilder.DropColumn(
                name: "detailImage",
                table: "MS_Detail");
        }
    }
}
