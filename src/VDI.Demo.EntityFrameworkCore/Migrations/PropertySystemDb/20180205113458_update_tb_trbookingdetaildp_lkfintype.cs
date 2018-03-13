using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class update_tb_trbookingdetaildp_lkfintype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "dpCalcID",
                table: "TR_BookingDetailDP",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isSetting",
                table: "TR_BookingDetailDP",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<short>(
                name: "monthsDue",
                table: "TR_BookingDetailDP",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "finStartM",
                table: "MS_TermPmt",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isSetting",
                table: "MS_TermPmt",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDetailDP_dpCalcID",
                table: "TR_BookingDetailDP",
                column: "dpCalcID");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_BookingDetailDP_LK_DPCalc_dpCalcID",
                table: "TR_BookingDetailDP",
                column: "dpCalcID",
                principalTable: "LK_DPCalc",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_BookingDetailDP_LK_DPCalc_dpCalcID",
                table: "TR_BookingDetailDP");

            migrationBuilder.DropIndex(
                name: "IX_TR_BookingDetailDP_dpCalcID",
                table: "TR_BookingDetailDP");

            migrationBuilder.DropColumn(
                name: "dpCalcID",
                table: "TR_BookingDetailDP");

            migrationBuilder.DropColumn(
                name: "isSetting",
                table: "TR_BookingDetailDP");

            migrationBuilder.DropColumn(
                name: "monthsDue",
                table: "TR_BookingDetailDP");

            migrationBuilder.DropColumn(
                name: "finStartM",
                table: "MS_TermPmt");

            migrationBuilder.DropColumn(
                name: "isSetting",
                table: "MS_TermPmt");
        }
    }
}
