using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class fixing_table_trUnitOrderHeader_OB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "TR_UnitOrderHeader",
                newName: "statusID");

            migrationBuilder.RenameColumn(
                name: "payType",
                table: "TR_UnitOrderHeader",
                newName: "paymentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitOrderHeader_paymentTypeID",
                table: "TR_UnitOrderHeader",
                column: "paymentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitOrderHeader_statusID",
                table: "TR_UnitOrderHeader",
                column: "statusID");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitOrderHeader_LK_PaymentType_paymentTypeID",
                table: "TR_UnitOrderHeader",
                column: "paymentTypeID",
                principalTable: "LK_PaymentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitOrderHeader_LK_BookingOnlineStatus_statusID",
                table: "TR_UnitOrderHeader",
                column: "statusID",
                principalTable: "LK_BookingOnlineStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitOrderHeader_LK_PaymentType_paymentTypeID",
                table: "TR_UnitOrderHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitOrderHeader_LK_BookingOnlineStatus_statusID",
                table: "TR_UnitOrderHeader");

            migrationBuilder.DropIndex(
                name: "IX_TR_UnitOrderHeader_paymentTypeID",
                table: "TR_UnitOrderHeader");

            migrationBuilder.DropIndex(
                name: "IX_TR_UnitOrderHeader_statusID",
                table: "TR_UnitOrderHeader");

            migrationBuilder.RenameColumn(
                name: "statusID",
                table: "TR_UnitOrderHeader",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "paymentTypeID",
                table: "TR_UnitOrderHeader",
                newName: "payType");
        }
    }
}
