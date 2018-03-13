using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.TAXDb
{
    public partial class add_3_table_tax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FP_TR_FPHeader",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    coCode = table.Column<string>(maxLength: 5, nullable: false),
                    FPCode = table.Column<string>(maxLength: 20, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DPAmount = table.Column<decimal>(type: "money", nullable: false),
                    FPBranchCode = table.Column<string>(maxLength: 3, nullable: false),
                    FPNo = table.Column<string>(maxLength: 8, nullable: false),
                    FPStatCode = table.Column<string>(maxLength: 1, nullable: false),
                    FPTransCode = table.Column<string>(maxLength: 2, nullable: false),
                    FPType = table.Column<string>(maxLength: 1, nullable: false),
                    FPYear = table.Column<string>(maxLength: 2, nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    NPWP = table.Column<string>(maxLength: 30, nullable: false),
                    accCode = table.Column<string>(maxLength: 5, nullable: false),
                    discAmount = table.Column<decimal>(type: "money", nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    payNo = table.Column<int>(nullable: false),
                    paymentCode = table.Column<string>(maxLength: 13, nullable: false),
                    pmtBatchNo = table.Column<string>(maxLength: 15, nullable: false),
                    priceType = table.Column<string>(maxLength: 1, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    rentalCode = table.Column<string>(maxLength: 10, nullable: false),
                    sourceCode = table.Column<string>(maxLength: 3, nullable: false),
                    transDate = table.Column<DateTime>(nullable: false),
                    transNo = table.Column<string>(maxLength: 20, nullable: false),
                    unitCode = table.Column<string>(maxLength: 16, nullable: false),
                    unitNo = table.Column<string>(maxLength: 8, nullable: false),
                    unitPriceAmt = table.Column<decimal>(type: "money", nullable: true),
                    unitPriceVat = table.Column<decimal>(type: "money", nullable: true),
                    userAddress = table.Column<string>(maxLength: 200, nullable: false),
                    vatAmt = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FP_TR_FPHeader", x => new { x.entityCode, x.coCode, x.FPCode });
                    table.UniqueConstraint("AK_FP_TR_FPHeader_coCode_entityCode_FPCode", x => new { x.coCode, x.entityCode, x.FPCode });
                });

            migrationBuilder.CreateTable(
                name: "msBatchPajakStock",
                columns: table => new
                {
                    BatchID = table.Column<int>(nullable: false),
                    CoCode = table.Column<string>(maxLength: 5, nullable: false),
                    FPBranchCode = table.Column<string>(maxLength: 3, nullable: false),
                    FPYear = table.Column<string>(maxLength: 2, nullable: false),
                    FPNo = table.Column<string>(maxLength: 8, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    FPStatCode = table.Column<string>(maxLength: 3, nullable: true),
                    FPTransCode = table.Column<string>(maxLength: 3, nullable: true),
                    IsAvailable = table.Column<bool>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    YearPeriod = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_msBatchPajakStock", x => new { x.BatchID, x.CoCode, x.FPBranchCode, x.FPYear, x.FPNo });
                    table.UniqueConstraint("AK_msBatchPajakStock_BatchID_CoCode_FPBranchCode_FPNo_FPYear", x => new { x.BatchID, x.CoCode, x.FPBranchCode, x.FPNo, x.FPYear });
                });

            migrationBuilder.CreateTable(
                name: "FP_TR_FPDetail",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    coCode = table.Column<string>(maxLength: 5, nullable: false),
                    FPCode = table.Column<string>(maxLength: 20, nullable: false),
                    transNo = table.Column<short>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    currencyCode = table.Column<string>(maxLength: 5, nullable: false),
                    currencyRate = table.Column<decimal>(type: "money", nullable: false),
                    transAmount = table.Column<decimal>(type: "money", nullable: false),
                    transDesc = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FP_TR_FPDetail", x => new { x.entityCode, x.coCode, x.FPCode, x.transNo });
                    table.UniqueConstraint("AK_FP_TR_FPDetail_coCode_entityCode_FPCode_transNo", x => new { x.coCode, x.entityCode, x.FPCode, x.transNo });
                    table.ForeignKey(
                        name: "FK_FP_TR_FPDetail_FP_TR_FPHeader_entityCode_coCode_FPCode",
                        columns: x => new { x.entityCode, x.coCode, x.FPCode },
                        principalTable: "FP_TR_FPHeader",
                        principalColumns: new[] { "entityCode", "coCode", "FPCode" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FP_TR_FPDetail");

            migrationBuilder.DropTable(
                name: "msBatchPajakStock");

            migrationBuilder.DropTable(
                name: "FP_TR_FPHeader");
        }
    }
}
