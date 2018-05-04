using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PersonalsNewDb
{
    public partial class Update_Composite_Only_ProvinceCode_MS_Province_AND_Add_FK_MS_City : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MS_Province",
                table: "MS_Province");

            migrationBuilder.AlterColumn<string>(
                name: "provinceName",
                table: "MS_Province",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "provinceCode",
                table: "MS_City",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MS_Province",
                table: "MS_Province",
                column: "provinceCode");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_City_MS_Province_provinceCode",
                table: "MS_City");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MS_Province",
                table: "MS_Province");

            migrationBuilder.DropIndex(
                name: "IX_MS_City_provinceCode",
                table: "MS_City");

            migrationBuilder.DropColumn(
                name: "provinceCode",
                table: "MS_City");

            migrationBuilder.AlterColumn<string>(
                name: "provinceName",
                table: "MS_Province",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MS_Province",
                table: "MS_Province",
                columns: new[] { "provinceCode", "provinceName" });
        }
    }
}
