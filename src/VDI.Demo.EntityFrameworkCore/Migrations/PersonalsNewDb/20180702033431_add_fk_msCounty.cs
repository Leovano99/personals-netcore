using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PersonalsNewDb
{
    public partial class add_fk_msCounty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "MS_County",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "countyCode",
                table: "MS_City",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MS_County_country",
                table: "MS_County",
                column: "country");

            migrationBuilder.AddForeignKey(
                name: "FK_MS_County_LK_Country_country",
                table: "MS_County",
                column: "country",
                principalTable: "LK_Country",
                principalColumn: "country",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_County_LK_Country_country",
                table: "MS_County");

            migrationBuilder.DropIndex(
                name: "IX_MS_County_country",
                table: "MS_County");

            migrationBuilder.DropColumn(
                name: "country",
                table: "MS_County");

            migrationBuilder.DropColumn(
                name: "countyCode",
                table: "MS_City");
        }
    }
}
