using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PersonalsNewDb
{
    public partial class fixing_fk_msCounty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "provinceCode",
                table: "MS_County",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Province_country",
                table: "MS_Province",
                column: "country");

            migrationBuilder.CreateIndex(
                name: "IX_MS_County_provinceCode",
                table: "MS_County",
                column: "provinceCode");

            migrationBuilder.AddForeignKey(
                name: "FK_MS_County_MS_Province_provinceCode",
                table: "MS_County",
                column: "provinceCode",
                principalTable: "MS_Province",
                principalColumn: "provinceCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MS_Province_LK_Country_country",
                table: "MS_Province",
                column: "country",
                principalTable: "LK_Country",
                principalColumn: "country",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_County_MS_Province_provinceCode",
                table: "MS_County");

            migrationBuilder.DropForeignKey(
                name: "FK_MS_Province_LK_Country_country",
                table: "MS_Province");

            migrationBuilder.DropIndex(
                name: "IX_MS_Province_country",
                table: "MS_Province");

            migrationBuilder.DropIndex(
                name: "IX_MS_County_provinceCode",
                table: "MS_County");

            migrationBuilder.DropColumn(
                name: "provinceCode",
                table: "MS_County");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "MS_County",
                maxLength: 100,
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
    }
}
