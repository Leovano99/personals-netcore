using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class add_table_tr_payment_bulk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TR_PaymentBulk",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    bookingHeaderID = table.Column<int>(nullable: false),
                    bulkPaymentKey = table.Column<string>(maxLength: 150, nullable: false),
                    clearDate = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    othersTypeID = table.Column<int>(nullable: false),
                    payForID = table.Column<int>(nullable: false),
                    payTypeID = table.Column<int>(nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    unitID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_PaymentBulk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_PaymentBulk_TR_BookingHeader_bookingHeaderID",
                        column: x => x.bookingHeaderID,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_PaymentBulk_LK_OthersType_othersTypeID",
                        column: x => x.othersTypeID,
                        principalTable: "LK_OthersType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_PaymentBulk_LK_PayFor_payForID",
                        column: x => x.payForID,
                        principalTable: "LK_PayFor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_PaymentBulk_LK_PayType_payTypeID",
                        column: x => x.payTypeID,
                        principalTable: "LK_PayType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_PaymentBulk_MS_Unit_unitID",
                        column: x => x.unitID,
                        principalTable: "MS_Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentBulk_bookingHeaderID",
                table: "TR_PaymentBulk",
                column: "bookingHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentBulk_othersTypeID",
                table: "TR_PaymentBulk",
                column: "othersTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentBulk_payForID",
                table: "TR_PaymentBulk",
                column: "payForID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentBulk_payTypeID",
                table: "TR_PaymentBulk",
                column: "payTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentBulk_unitID",
                table: "TR_PaymentBulk",
                column: "unitID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TR_PaymentBulk");
        }
    }
}
