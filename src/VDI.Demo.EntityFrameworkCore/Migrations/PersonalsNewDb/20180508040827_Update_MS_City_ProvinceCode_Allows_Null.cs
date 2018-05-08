using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PersonalsNewDb
{
    public partial class Update_MS_City_ProvinceCode_Allows_Null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_City_MS_Province_provinceCode",
                table: "MS_City");

            migrationBuilder.DropIndex(
                name: "IX_MS_City_provinceCode",
                table: "MS_City");

            migrationBuilder.AlterColumn<string>(
                name: "provinceCode",
                table: "MS_City",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "provinceCode",
                table: "MS_City",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MS_City_provinceCode",
                table: "MS_City",
                column: "provinceCode");

            migrationBuilder.AddForeignKey(
                name: "FK_MS_City_MS_Province_provinceCode",
                table: "MS_City",
                column: "provinceCode",
                principalTable: "MS_Province",
                principalColumn: "provinceCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
