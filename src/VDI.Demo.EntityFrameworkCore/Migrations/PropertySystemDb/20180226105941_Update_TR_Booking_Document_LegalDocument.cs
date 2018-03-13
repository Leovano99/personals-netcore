using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class Update_TR_Booking_Document_LegalDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "docCode",
                table: "TR_BookingDocument");

            migrationBuilder.AddColumn<int>(
                name: "docID",
                table: "TR_BookingDocument",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "oldDocNo",
                table: "TR_BookingDocument",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tandaTerimaBy",
                table: "TR_BookingDocument",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "tandaTerimaDate",
                table: "TR_BookingDocument",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tandaTerimaFile",
                table: "TR_BookingDocument",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tandaTerimaNo",
                table: "TR_BookingDocument",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDocument_docID",
                table: "TR_BookingDocument",
                column: "docID");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_BookingDocument_MS_Document_docID",
                table: "TR_BookingDocument",
                column: "docID",
                principalTable: "MS_Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_BookingDocument_MS_Document_docID",
                table: "TR_BookingDocument");

            migrationBuilder.DropIndex(
                name: "IX_TR_BookingDocument_docID",
                table: "TR_BookingDocument");

            migrationBuilder.DropColumn(
                name: "docID",
                table: "TR_BookingDocument");

            migrationBuilder.DropColumn(
                name: "oldDocNo",
                table: "TR_BookingDocument");

            migrationBuilder.DropColumn(
                name: "tandaTerimaBy",
                table: "TR_BookingDocument");

            migrationBuilder.DropColumn(
                name: "tandaTerimaDate",
                table: "TR_BookingDocument");

            migrationBuilder.DropColumn(
                name: "tandaTerimaFile",
                table: "TR_BookingDocument");

            migrationBuilder.DropColumn(
                name: "tandaTerimaNo",
                table: "TR_BookingDocument");

            migrationBuilder.AddColumn<string>(
                name: "docCode",
                table: "TR_BookingDocument",
                maxLength: 5,
                nullable: false,
                defaultValue: "");
        }
    }
}
