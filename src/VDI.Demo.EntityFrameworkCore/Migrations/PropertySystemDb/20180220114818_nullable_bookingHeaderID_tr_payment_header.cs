using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class nullable_bookingHeaderID_tr_payment_header : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "bookingHeaderID",
                table: "TR_PaymentHeader",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "bookingHeaderID",
                table: "TR_PaymentHeader",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
