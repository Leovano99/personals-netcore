using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class create_table_lkPaymentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitOrderDetail_MS_Renovation_renovID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitReserved_MS_Renovation_renovID",
                table: "TR_UnitReserved");

            migrationBuilder.DropIndex(
                name: "IX_TR_UnitReserved_renovID",
                table: "TR_UnitReserved");

            migrationBuilder.DropIndex(
                name: "IX_TR_UnitOrderDetail_renovID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropColumn(
                name: "renovID",
                table: "TR_UnitReserved");

            migrationBuilder.DropColumn(
                name: "userRefNo",
                table: "TR_UnitReserved");

            migrationBuilder.DropColumn(
                name: "userRef",
                table: "TR_UnitOrderHeader");

            migrationBuilder.DropColumn(
                name: "renovID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.AddColumn<string>(
                name: "renovCode",
                table: "TR_UnitReserved",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "userID",
                table: "TR_UnitReserved",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "userID",
                table: "TR_UnitOrderHeader",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "renovCode",
                table: "TR_UnitOrderDetail",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LK_PaymentType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    paymentTypeName = table.Column<string>(maxLength: 50, nullable: false),
                    sortNo = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_PaymentType", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LK_PaymentType");

            migrationBuilder.DropColumn(
                name: "renovCode",
                table: "TR_UnitReserved");

            migrationBuilder.DropColumn(
                name: "userID",
                table: "TR_UnitReserved");

            migrationBuilder.DropColumn(
                name: "userID",
                table: "TR_UnitOrderHeader");

            migrationBuilder.DropColumn(
                name: "renovCode",
                table: "TR_UnitOrderDetail");

            migrationBuilder.AddColumn<int>(
                name: "renovID",
                table: "TR_UnitReserved",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userRefNo",
                table: "TR_UnitReserved",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userRef",
                table: "TR_UnitOrderHeader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "renovID",
                table: "TR_UnitOrderDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitReserved_renovID",
                table: "TR_UnitReserved",
                column: "renovID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitOrderDetail_renovID",
                table: "TR_UnitOrderDetail",
                column: "renovID");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitOrderDetail_MS_Renovation_renovID",
                table: "TR_UnitOrderDetail",
                column: "renovID",
                principalTable: "MS_Renovation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitReserved_MS_Renovation_renovID",
                table: "TR_UnitReserved",
                column: "renovID",
                principalTable: "MS_Renovation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
