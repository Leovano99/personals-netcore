using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.NewCommDb
{
    public partial class alter_table_trSoldUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_SoldUnit_MS_Developer_Schema_developerSchemaID",
                table: "TR_SoldUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_SoldUnitRequirement_MS_Developer_Schema_developerSchemaID",
                table: "TR_SoldUnitRequirement");

            migrationBuilder.DropIndex(
                name: "IX_TR_SoldUnitRequirement_developerSchemaID",
                table: "TR_SoldUnitRequirement");

            migrationBuilder.DropIndex(
                name: "IX_TR_SoldUnit_developerSchemaID",
                table: "TR_SoldUnit");

            migrationBuilder.DropColumn(
                name: "developerSchemaID",
                table: "TR_SoldUnitRequirement");

            migrationBuilder.DropColumn(
                name: "developerSchemaID",
                table: "TR_SoldUnit");

            migrationBuilder.AddColumn<string>(
                name: "devCode",
                table: "TR_SoldUnitRequirement",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "devCode",
                table: "TR_SoldUnit",
                maxLength: 5,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "devCode",
                table: "TR_SoldUnitRequirement");

            migrationBuilder.DropColumn(
                name: "devCode",
                table: "TR_SoldUnit");

            migrationBuilder.AddColumn<int>(
                name: "developerSchemaID",
                table: "TR_SoldUnitRequirement",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "developerSchemaID",
                table: "TR_SoldUnit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TR_SoldUnitRequirement_developerSchemaID",
                table: "TR_SoldUnitRequirement",
                column: "developerSchemaID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_SoldUnit_developerSchemaID",
                table: "TR_SoldUnit",
                column: "developerSchemaID");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_SoldUnit_MS_Developer_Schema_developerSchemaID",
                table: "TR_SoldUnit",
                column: "developerSchemaID",
                principalTable: "MS_Developer_Schema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_SoldUnitRequirement_MS_Developer_Schema_developerSchemaID",
                table: "TR_SoldUnitRequirement",
                column: "developerSchemaID",
                principalTable: "MS_Developer_Schema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
