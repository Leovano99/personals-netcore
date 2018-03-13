using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class update_tb_trbookingitemprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "termNo",
                table: "TR_BookingItemPrice");

            migrationBuilder.AddColumn<int>(
                name: "termID",
                table: "TR_BookingItemPrice",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingItemPrice_termID",
                table: "TR_BookingItemPrice",
                column: "termID");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_BookingItemPrice_MS_Term_termID",
                table: "TR_BookingItemPrice",
                column: "termID",
                principalTable: "MS_Term",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_BookingItemPrice_MS_Term_termID",
                table: "TR_BookingItemPrice");

            migrationBuilder.DropIndex(
                name: "IX_TR_BookingItemPrice_termID",
                table: "TR_BookingItemPrice");

            migrationBuilder.DropColumn(
                name: "termID",
                table: "TR_BookingItemPrice");

            migrationBuilder.AddColumn<short>(
                name: "termNo",
                table: "TR_BookingItemPrice",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
