using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class added_table_5_lippoMaster_tr_bookingHeaderTerm_dll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MS_TaxType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    taxTypeCode = table.Column<string>(maxLength: 5, nullable: true),
                    taxTypeDesc = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_TaxType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingHeaderTerm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DPCalcType = table.Column<string>(maxLength: 1, nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PPJBDue = table.Column<int>(nullable: false),
                    addDisc = table.Column<double>(nullable: false),
                    addDiscNo = table.Column<int>(nullable: false),
                    bookingDetailID = table.Column<int>(nullable: false),
                    discBFCalcType = table.Column<string>(maxLength: 1, nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    finStartDue = table.Column<int>(nullable: false),
                    finTypeCode = table.Column<string>(maxLength: 5, nullable: true),
                    isActive = table.Column<bool>(nullable: false),
                    remarks = table.Column<string>(maxLength: 200, nullable: true),
                    termID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingHeaderTerm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingHeaderTerm_TR_BookingDetail_bookingDetailID",
                        column: x => x.bookingDetailID,
                        principalTable: "TR_BookingDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_BookingHeaderTerm_MS_Term_termID",
                        column: x => x.termID,
                        principalTable: "MS_Term",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TR_CashAddDisc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    addDisc = table.Column<double>(nullable: false),
                    addDiscDesc = table.Column<string>(maxLength: 500, nullable: true),
                    addDiscNo = table.Column<byte>(nullable: false),
                    bookNo = table.Column<int>(nullable: false),
                    bookingDetailID = table.Column<int>(nullable: false),
                    coCode = table.Column<string>(maxLength: 5, nullable: false),
                    entityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_CashAddDisc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_CashAddDisc_TR_BookingDetail_bookingDetailID",
                        column: x => x.bookingDetailID,
                        principalTable: "TR_BookingDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_MKTAddDisc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    addDisc = table.Column<double>(nullable: false),
                    addDiscDesc = table.Column<string>(maxLength: 500, nullable: true),
                    addDiscNo = table.Column<byte>(nullable: false),
                    bookNo = table.Column<int>(nullable: false),
                    bookingDetailID = table.Column<int>(nullable: false),
                    coCode = table.Column<string>(maxLength: 5, nullable: false),
                    entityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_MKTAddDisc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_MKTAddDisc_TR_BookingDetail_bookingDetailID",
                        column: x => x.bookingDetailID,
                        principalTable: "TR_BookingDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingTax",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    bookingDetailID = table.Column<int>(nullable: false),
                    taxTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingTax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingTax_TR_BookingDetail_bookingDetailID",
                        column: x => x.bookingDetailID,
                        principalTable: "TR_BookingDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_BookingTax_MS_TaxType_taxTypeID",
                        column: x => x.taxTypeID,
                        principalTable: "MS_TaxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingHeaderTerm_bookingDetailID",
                table: "TR_BookingHeaderTerm",
                column: "bookingDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingHeaderTerm_termID",
                table: "TR_BookingHeaderTerm",
                column: "termID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingTax_bookingDetailID",
                table: "TR_BookingTax",
                column: "bookingDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingTax_taxTypeID",
                table: "TR_BookingTax",
                column: "taxTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_CashAddDisc_bookingDetailID",
                table: "TR_CashAddDisc",
                column: "bookingDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_MKTAddDisc_bookingDetailID",
                table: "TR_MKTAddDisc",
                column: "bookingDetailID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TR_BookingHeaderTerm");

            migrationBuilder.DropTable(
                name: "TR_BookingTax");

            migrationBuilder.DropTable(
                name: "TR_CashAddDisc");

            migrationBuilder.DropTable(
                name: "TR_MKTAddDisc");

            migrationBuilder.DropTable(
                name: "MS_TaxType");
        }
    }
}
