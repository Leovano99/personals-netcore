using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class FixingTbl_bookingHeaderTerm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_BookingHeaderTerm_TR_BookingDetail_bookingDetailID",
                table: "TR_BookingHeaderTerm");

            migrationBuilder.RenameColumn(
                name: "bookingDetailID",
                table: "TR_BookingHeaderTerm",
                newName: "bookingHeaderID");

            migrationBuilder.RenameIndex(
                name: "IX_TR_BookingHeaderTerm_bookingDetailID",
                table: "TR_BookingHeaderTerm",
                newName: "IX_TR_BookingHeaderTerm_bookingHeaderID");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_BookingHeaderTerm_TR_BookingHeader_bookingHeaderID",
                table: "TR_BookingHeaderTerm",
                column: "bookingHeaderID",
                principalTable: "TR_BookingHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_BookingHeaderTerm_TR_BookingHeader_bookingHeaderID",
                table: "TR_BookingHeaderTerm");

            migrationBuilder.RenameColumn(
                name: "bookingHeaderID",
                table: "TR_BookingHeaderTerm",
                newName: "bookingDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_TR_BookingHeaderTerm_bookingHeaderID",
                table: "TR_BookingHeaderTerm",
                newName: "IX_TR_BookingHeaderTerm_bookingDetailID");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_BookingHeaderTerm_TR_BookingDetail_bookingDetailID",
                table: "TR_BookingHeaderTerm",
                column: "bookingDetailID",
                principalTable: "TR_BookingDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
