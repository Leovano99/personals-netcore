using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class addfield_5table_setup_product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "projectID",
                table: "MS_UnitCode",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "remarks",
                table: "MS_TermMain",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "sortNo",
                table: "MS_Term",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "startPenaltyDay",
                table: "MS_Project",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "penaltyRate",
                table: "MS_Project",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<double>(
                name: "penaltyRate",
                table: "MS_Cluster",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "projectID",
                table: "MS_Cluster",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "startPenaltyDay",
                table: "MS_Cluster",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MS_UnitCode_projectID",
                table: "MS_UnitCode",
                column: "projectID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Cluster_projectID",
                table: "MS_Cluster",
                column: "projectID");

            migrationBuilder.AddForeignKey(
                name: "FK_MS_Cluster_MS_Project_projectID",
                table: "MS_Cluster",
                column: "projectID",
                principalTable: "MS_Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MS_UnitCode_MS_Project_projectID",
                table: "MS_UnitCode",
                column: "projectID",
                principalTable: "MS_Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_Cluster_MS_Project_projectID",
                table: "MS_Cluster");

            migrationBuilder.DropForeignKey(
                name: "FK_MS_UnitCode_MS_Project_projectID",
                table: "MS_UnitCode");

            migrationBuilder.DropIndex(
                name: "IX_MS_UnitCode_projectID",
                table: "MS_UnitCode");

            migrationBuilder.DropIndex(
                name: "IX_MS_Cluster_projectID",
                table: "MS_Cluster");

            migrationBuilder.DropColumn(
                name: "projectID",
                table: "MS_UnitCode");

            migrationBuilder.DropColumn(
                name: "remarks",
                table: "MS_TermMain");

            migrationBuilder.DropColumn(
                name: "sortNo",
                table: "MS_Term");

            migrationBuilder.DropColumn(
                name: "penaltyRate",
                table: "MS_Cluster");

            migrationBuilder.DropColumn(
                name: "projectID",
                table: "MS_Cluster");

            migrationBuilder.DropColumn(
                name: "startPenaltyDay",
                table: "MS_Cluster");

            migrationBuilder.AlterColumn<int>(
                name: "startPenaltyDay",
                table: "MS_Project",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "penaltyRate",
                table: "MS_Project",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
