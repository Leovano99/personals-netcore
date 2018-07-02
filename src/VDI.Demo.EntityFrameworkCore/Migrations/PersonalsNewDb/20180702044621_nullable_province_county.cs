using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PersonalsNewDb
{
    public partial class nullable_province_county : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_County_MS_Province_provinceCode",
                table: "MS_County");

            migrationBuilder.AlterColumn<string>(
                name: "provinceCode",
                table: "MS_County",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "countyCode",
                table: "MS_City",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5);

            migrationBuilder.AddForeignKey(
                name: "FK_MS_County_MS_Province_provinceCode",
                table: "MS_County",
                column: "provinceCode",
                principalTable: "MS_Province",
                principalColumn: "provinceCode",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_County_MS_Province_provinceCode",
                table: "MS_County");

            migrationBuilder.AlterColumn<string>(
                name: "provinceCode",
                table: "MS_County",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "countyCode",
                table: "MS_City",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MS_County_MS_Province_provinceCode",
                table: "MS_County",
                column: "provinceCode",
                principalTable: "MS_Province",
                principalColumn: "provinceCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
