using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class create_table_trPaymentOnlineBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitOrderDetail_TR_UnitOrderHeader_orderID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.RenameColumn(
                name: "orderID",
                table: "TR_UnitOrderDetail",
                newName: "UnitOrderHeaderID");

            migrationBuilder.RenameIndex(
                name: "IX_TR_UnitOrderDetail_orderID",
                table: "TR_UnitOrderDetail",
                newName: "IX_TR_UnitOrderDetail_UnitOrderHeaderID");

            migrationBuilder.CreateTable(
                name: "TR_PaymentOnlineBook",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    UnitOrderHeaderID = table.Column<int>(nullable: false),
                    bankAccName = table.Column<string>(maxLength: 100, nullable: true),
                    bankAccNo = table.Column<string>(maxLength: 100, nullable: true),
                    bankName = table.Column<string>(maxLength: 100, nullable: true),
                    offlineType = table.Column<string>(maxLength: 100, nullable: true),
                    paymentAmt = table.Column<decimal>(type: "money", nullable: false),
                    paymentDate = table.Column<DateTime>(nullable: false),
                    paymentType = table.Column<string>(maxLength: 200, nullable: false),
                    resiImage = table.Column<string>(maxLength: 300, nullable: true),
                    resiNo = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_PaymentOnlineBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_PaymentOnlineBook_TR_UnitOrderHeader_UnitOrderHeaderID",
                        column: x => x.UnitOrderHeaderID,
                        principalTable: "TR_UnitOrderHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentOnlineBook_UnitOrderHeaderID",
                table: "TR_PaymentOnlineBook",
                column: "UnitOrderHeaderID");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitOrderDetail_TR_UnitOrderHeader_UnitOrderHeaderID",
                table: "TR_UnitOrderDetail",
                column: "UnitOrderHeaderID",
                principalTable: "TR_UnitOrderHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitOrderDetail_TR_UnitOrderHeader_UnitOrderHeaderID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropTable(
                name: "TR_PaymentOnlineBook");

            migrationBuilder.RenameColumn(
                name: "UnitOrderHeaderID",
                table: "TR_UnitOrderDetail",
                newName: "orderID");

            migrationBuilder.RenameIndex(
                name: "IX_TR_UnitOrderDetail_UnitOrderHeaderID",
                table: "TR_UnitOrderDetail",
                newName: "IX_TR_UnitOrderDetail_orderID");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitOrderDetail_TR_UnitOrderHeader_orderID",
                table: "TR_UnitOrderDetail",
                column: "orderID",
                principalTable: "TR_UnitOrderHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
