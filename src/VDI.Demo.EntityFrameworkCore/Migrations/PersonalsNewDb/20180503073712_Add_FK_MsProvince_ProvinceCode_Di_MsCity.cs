using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PersonalsNewDb
{
    public partial class Add_FK_MsProvince_ProvinceCode_Di_MsCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MS_ProvinceprovinceCode",
                table: "MS_City",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MS_ProvinceprovinceName",
                table: "MS_City",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "provinceCode",
                table: "MS_City",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MS_City_MS_ProvinceprovinceCode_MS_ProvinceprovinceName",
                table: "MS_City",
                columns: new[] { "MS_ProvinceprovinceCode", "MS_ProvinceprovinceName" });

            migrationBuilder.AddForeignKey(
                name: "FK_MS_City_MS_Province_MS_ProvinceprovinceCode_MS_ProvinceprovinceName",
                table: "MS_City",
                columns: new[] { "MS_ProvinceprovinceCode", "MS_ProvinceprovinceName" },
                principalTable: "MS_Province",
                principalColumns: new[] { "provinceCode", "provinceName" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_City_MS_Province_MS_ProvinceprovinceCode_MS_ProvinceprovinceName",
                table: "MS_City");

            migrationBuilder.DropIndex(
                name: "IX_MS_City_MS_ProvinceprovinceCode_MS_ProvinceprovinceName",
                table: "MS_City");

            migrationBuilder.DropColumn(
                name: "MS_ProvinceprovinceCode",
                table: "MS_City");

            migrationBuilder.DropColumn(
                name: "MS_ProvinceprovinceName",
                table: "MS_City");

            migrationBuilder.DropColumn(
                name: "provinceCode",
                table: "MS_City");
        }
    }
}
