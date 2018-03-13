using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class addTableLK_PayFor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_PenaltySchedule_TR_BookingHeader_bookingHeader_ID",
                table: "TR_PenaltySchedule");

            migrationBuilder.DropColumn(
                name: "payForCode",
                table: "TR_PaymentHeader");

            migrationBuilder.DropColumn(
                name: "payForCode",
                table: "LK_Alloc");

            migrationBuilder.RenameColumn(
                name: "bookingHeader_ID",
                table: "TR_PenaltySchedule",
                newName: "bookingHeaderID");

            migrationBuilder.RenameIndex(
                name: "IX_TR_PenaltySchedule_bookingHeader_ID",
                table: "TR_PenaltySchedule",
                newName: "IX_TR_PenaltySchedule_bookingHeaderID");

            migrationBuilder.AddColumn<int>(
                name: "payForID",
                table: "TR_PaymentHeader",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "payForID",
                table: "LK_Alloc",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LK_PayFor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    isIncome = table.Column<bool>(nullable: false),
                    isInventory = table.Column<bool>(nullable: false),
                    isSched = table.Column<bool>(nullable: false),
                    payForName = table.Column<string>(maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_PayFor", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentHeader_payForID",
                table: "TR_PaymentHeader",
                column: "payForID");

            migrationBuilder.CreateIndex(
                name: "IX_LK_Alloc_payForID",
                table: "LK_Alloc",
                column: "payForID");

            migrationBuilder.AddForeignKey(
                name: "FK_LK_Alloc_LK_PayFor_payForID",
                table: "LK_Alloc",
                column: "payForID",
                principalTable: "LK_PayFor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_PaymentHeader_LK_PayFor_payForID",
                table: "TR_PaymentHeader",
                column: "payForID",
                principalTable: "LK_PayFor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_PenaltySchedule_TR_BookingHeader_bookingHeaderID",
                table: "TR_PenaltySchedule",
                column: "bookingHeaderID",
                principalTable: "TR_BookingHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LK_Alloc_LK_PayFor_payForID",
                table: "LK_Alloc");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_PaymentHeader_LK_PayFor_payForID",
                table: "TR_PaymentHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_PenaltySchedule_TR_BookingHeader_bookingHeaderID",
                table: "TR_PenaltySchedule");

            migrationBuilder.DropTable(
                name: "LK_PayFor");

            migrationBuilder.DropIndex(
                name: "IX_TR_PaymentHeader_payForID",
                table: "TR_PaymentHeader");

            migrationBuilder.DropIndex(
                name: "IX_LK_Alloc_payForID",
                table: "LK_Alloc");

            migrationBuilder.DropColumn(
                name: "payForID",
                table: "TR_PaymentHeader");

            migrationBuilder.DropColumn(
                name: "payForID",
                table: "LK_Alloc");

            migrationBuilder.RenameColumn(
                name: "bookingHeaderID",
                table: "TR_PenaltySchedule",
                newName: "bookingHeader_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TR_PenaltySchedule_bookingHeaderID",
                table: "TR_PenaltySchedule",
                newName: "IX_TR_PenaltySchedule_bookingHeader_ID");

            migrationBuilder.AddColumn<string>(
                name: "payForCode",
                table: "TR_PaymentHeader",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "payForCode",
                table: "LK_Alloc",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_PenaltySchedule_TR_BookingHeader_bookingHeader_ID",
                table: "TR_PenaltySchedule",
                column: "bookingHeader_ID",
                principalTable: "TR_BookingHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
