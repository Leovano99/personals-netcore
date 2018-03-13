using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class fixing_tr_booking_header : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_BookingHeader_MS_TransFrom_TR_TransfromId",
                table: "TR_BookingHeader");

            migrationBuilder.RenameColumn(
                name: "TR_TransfromId",
                table: "TR_BookingHeader",
                newName: "MS_TransFromId");

            migrationBuilder.RenameIndex(
                name: "IX_TR_BookingHeader_TR_TransfromId",
                table: "TR_BookingHeader",
                newName: "IX_TR_BookingHeader_MS_TransFromId");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_BookingHeader_MS_TransFrom_MS_TransFromId",
                table: "TR_BookingHeader",
                column: "MS_TransFromId",
                principalTable: "MS_TransFrom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_BookingHeader_MS_TransFrom_MS_TransFromId",
                table: "TR_BookingHeader");

            migrationBuilder.RenameColumn(
                name: "MS_TransFromId",
                table: "TR_BookingHeader",
                newName: "TR_TransfromId");

            migrationBuilder.RenameIndex(
                name: "IX_TR_BookingHeader_MS_TransFromId",
                table: "TR_BookingHeader",
                newName: "IX_TR_BookingHeader_TR_TransfromId");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_BookingHeader_MS_TransFrom_TR_TransfromId",
                table: "TR_BookingHeader",
                column: "TR_TransfromId",
                principalTable: "MS_TransFrom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
