using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class nullable_bookingHeaderID_in_tr_payment_bulk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_PaymentBulk_TR_BookingHeader_bookingHeaderID",
                table: "TR_PaymentBulk");

            migrationBuilder.AlterColumn<int>(
                name: "bookingHeaderID",
                table: "TR_PaymentBulk",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_TR_PaymentBulk_TR_BookingHeader_bookingHeaderID",
                table: "TR_PaymentBulk",
                column: "bookingHeaderID",
                principalTable: "TR_BookingHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_PaymentBulk_TR_BookingHeader_bookingHeaderID",
                table: "TR_PaymentBulk");

            migrationBuilder.AlterColumn<int>(
                name: "bookingHeaderID",
                table: "TR_PaymentBulk",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_PaymentBulk_TR_BookingHeader_bookingHeaderID",
                table: "TR_PaymentBulk",
                column: "bookingHeaderID",
                principalTable: "TR_BookingHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
