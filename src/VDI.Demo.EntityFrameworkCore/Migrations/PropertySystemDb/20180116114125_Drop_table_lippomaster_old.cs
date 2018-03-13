using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class Drop_table_lippomaster_old : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TR_BookingDetail");

            migrationBuilder.DropTable(
                name: "TR_BookingDetailSchedule");

            migrationBuilder.DropTable(
                name: "TR_BookingDocument");

            migrationBuilder.DropTable(
                name: "TR_PaymentDetailAlloc");

            migrationBuilder.DropTable(
                name: "TR_PaymentDetailSchedule");

            migrationBuilder.DropTable(
                name: "TR_PaymentDetail");

            migrationBuilder.DropTable(
                name: "TR_PaymentHeader");

            migrationBuilder.DropTable(
                name: "TR_BookingHeader");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TR_BookingDetailSchedule",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    allocCode = table.Column<string>(maxLength: 3, nullable: false),
                    bookCode = table.Column<string>(maxLength: 20, nullable: true),
                    dueDate = table.Column<DateTime>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    netAmt = table.Column<decimal>(type: "money", nullable: false),
                    netOut = table.Column<decimal>(type: "money", nullable: false),
                    refNo = table.Column<short>(nullable: false),
                    remarks = table.Column<string>(maxLength: 100, nullable: false),
                    schedNo = table.Column<short>(nullable: false),
                    vatAmt = table.Column<decimal>(type: "money", nullable: false),
                    vatOut = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingDetailSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bookCode = table.Column<string>(maxLength: 20, nullable: true),
                    docCode = table.Column<string>(maxLength: 5, nullable: true),
                    docDate = table.Column<DateTime>(nullable: false),
                    docNo = table.Column<string>(maxLength: 50, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<string>(maxLength: 40, nullable: false),
                    modifTime = table.Column<DateTime>(nullable: false),
                    modifUN = table.Column<string>(maxLength: 40, nullable: false),
                    remarks = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingDocument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BFPayTypeCode = table.Column<string>(maxLength: 3, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DPCalcType = table.Column<string>(maxLength: 1, nullable: false),
                    KPRBankCode = table.Column<string>(maxLength: 5, nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    NUP = table.Column<string>(maxLength: 20, nullable: false),
                    PPJBDue = table.Column<short>(nullable: false),
                    SADStatus = table.Column<string>(maxLength: 1, nullable: false),
                    bankName = table.Column<string>(maxLength: 50, nullable: false),
                    bankNo = table.Column<string>(maxLength: 30, nullable: false),
                    bankRekeningPemilik = table.Column<string>(maxLength: 50, nullable: true),
                    bookCode = table.Column<string>(maxLength: 20, nullable: true),
                    bookDate = table.Column<DateTime>(nullable: false),
                    cancelDate = table.Column<DateTime>(nullable: true),
                    discBFCalcType = table.Column<string>(maxLength: 1, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    facadeCode = table.Column<string>(maxLength: 5, nullable: true),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<string>(maxLength: 40, nullable: false),
                    isPenaltyStop = table.Column<bool>(nullable: false),
                    isSK = table.Column<bool>(nullable: false),
                    isSMS = table.Column<bool>(nullable: false),
                    memberCode = table.Column<string>(maxLength: 12, nullable: false),
                    memberName = table.Column<string>(maxLength: 100, nullable: false),
                    modifTime = table.Column<DateTime>(nullable: false),
                    modifUN = table.Column<string>(maxLength: 40, nullable: false),
                    netPriceComm = table.Column<decimal>(type: "money", nullable: false),
                    nomorRekeningPemilik = table.Column<string>(maxLength: 50, nullable: true),
                    promotionCode = table.Column<string>(maxLength: 50, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    remarks = table.Column<string>(maxLength: 1500, nullable: false),
                    salesEvent = table.Column<string>(maxLength: 5, nullable: false),
                    scmCode = table.Column<string>(maxLength: 3, nullable: false),
                    shopBusinessCode = table.Column<string>(maxLength: 3, nullable: false),
                    shopName = table.Column<string>(maxLength: 50, nullable: false),
                    sumberDanaCode = table.Column<string>(maxLength: 3, nullable: true),
                    termCode = table.Column<string>(maxLength: 5, nullable: false),
                    termNo = table.Column<short>(nullable: false),
                    termRemarks = table.Column<string>(maxLength: 200, nullable: false),
                    transCode = table.Column<string>(maxLength: 5, nullable: false),
                    tujuanTransaksiCode = table.Column<string>(maxLength: 3, nullable: true),
                    unitID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TR_PaymentDetailSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    allocCode = table.Column<string>(maxLength: 3, nullable: false),
                    bookCode = table.Column<string>(maxLength: 20, nullable: true),
                    dueDate = table.Column<DateTime>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    netAmt = table.Column<decimal>(type: "money", nullable: false),
                    netOut = table.Column<decimal>(type: "money", nullable: false),
                    refNo = table.Column<short>(nullable: false),
                    remarks = table.Column<string>(maxLength: 100, nullable: false),
                    schedNo = table.Column<short>(nullable: false),
                    vatAmt = table.Column<decimal>(type: "money", nullable: false),
                    vatOut = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_PaymentDetailSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BFAmount = table.Column<decimal>(type: "money", nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TR_BookingHeaderId = table.Column<int>(nullable: true),
                    adjArea = table.Column<double>(nullable: false),
                    adjPrice = table.Column<decimal>(type: "money", nullable: false),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    amountComm = table.Column<decimal>(type: "money", nullable: false),
                    amountMKT = table.Column<decimal>(type: "money", nullable: false),
                    area = table.Column<double>(nullable: false),
                    bookCode = table.Column<string>(maxLength: 20, nullable: true),
                    bookNo = table.Column<int>(nullable: false),
                    coCode = table.Column<string>(maxLength: 5, nullable: false),
                    combineCode = table.Column<string>(maxLength: 1, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    finStartDue = table.Column<short>(nullable: false),
                    finTypeCode = table.Column<string>(maxLength: 5, nullable: true),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<string>(maxLength: 40, nullable: false),
                    itemCode = table.Column<string>(maxLength: 2, nullable: false),
                    modifTime = table.Column<DateTime>(nullable: false),
                    modifUN = table.Column<string>(maxLength: 40, nullable: false),
                    netNetPrice = table.Column<decimal>(type: "money", nullable: false),
                    netPrice = table.Column<decimal>(type: "money", nullable: false),
                    netPriceCash = table.Column<decimal>(type: "money", nullable: true),
                    netPriceComm = table.Column<decimal>(type: "money", nullable: false),
                    netPriceMKT = table.Column<decimal>(type: "money", nullable: false),
                    pctDisc = table.Column<double>(nullable: false),
                    pctTax = table.Column<double>(nullable: false),
                    refNo = table.Column<short>(nullable: false),
                    termNo = table.Column<int>(nullable: false),
                    trType = table.Column<string>(maxLength: 2, nullable: false),
                    unitID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingDetail_TR_BookingHeader_TR_BookingHeaderId",
                        column: x => x.TR_BookingHeaderId,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TR_PaymentHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    SMSSentTime = table.Column<DateTime>(nullable: true),
                    accCode = table.Column<string>(maxLength: 5, nullable: true),
                    apvTime = table.Column<DateTime>(nullable: true),
                    apvUN = table.Column<string>(maxLength: 50, nullable: true),
                    bookingHeaderID = table.Column<int>(nullable: false),
                    clearDate = table.Column<DateTime>(nullable: true),
                    combineCode = table.Column<string>(maxLength: 1, nullable: false),
                    controlNo = table.Column<string>(maxLength: 18, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    hadmail = table.Column<bool>(nullable: true),
                    isSMS = table.Column<bool>(nullable: false),
                    ket = table.Column<string>(maxLength: 300, nullable: false),
                    mailTime = table.Column<DateTime>(nullable: true),
                    payForCode = table.Column<string>(maxLength: 3, nullable: false),
                    paymentDate = table.Column<DateTime>(nullable: false),
                    transNo = table.Column<string>(maxLength: 18, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_PaymentHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_PaymentHeader_TR_BookingHeader_bookingHeaderID",
                        column: x => x.bookingHeaderID,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_PaymentDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    accCode = table.Column<string>(maxLength: 5, nullable: true),
                    bankName = table.Column<string>(maxLength: 30, nullable: false),
                    chequeNo = table.Column<string>(maxLength: 30, nullable: false),
                    dueDate = table.Column<DateTime>(nullable: true),
                    ket = table.Column<string>(maxLength: 300, nullable: false),
                    othersTypeCode = table.Column<string>(maxLength: 3, nullable: false),
                    payNo = table.Column<int>(nullable: false),
                    payTypeCode = table.Column<string>(maxLength: 3, nullable: false),
                    paymentHeaderID = table.Column<int>(nullable: false),
                    status = table.Column<string>(maxLength: 1, nullable: false),
                    transNo = table.Column<string>(maxLength: 18, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_PaymentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_PaymentDetail_TR_PaymentHeader_paymentHeaderID",
                        column: x => x.paymentHeaderID,
                        principalTable: "TR_PaymentHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_PaymentDetailAlloc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    accCode = table.Column<string>(maxLength: 5, nullable: true),
                    bookCode = table.Column<string>(maxLength: 20, nullable: true),
                    entityID = table.Column<string>(maxLength: 1, nullable: true),
                    netAmt = table.Column<decimal>(type: "money", nullable: false),
                    payNo = table.Column<int>(nullable: false),
                    paymentDetailID = table.Column<int>(nullable: false),
                    schedNo = table.Column<short>(nullable: false),
                    transNo = table.Column<string>(maxLength: 18, nullable: true),
                    vatAmt = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_PaymentDetailAlloc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_PaymentDetailAlloc_TR_PaymentDetail_paymentDetailID",
                        column: x => x.paymentDetailID,
                        principalTable: "TR_PaymentDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDetail_TR_BookingHeaderId",
                table: "TR_BookingDetail",
                column: "TR_BookingHeaderId");

            migrationBuilder.CreateIndex(
                name: "bookCodeUnique",
                table: "TR_BookingHeader",
                column: "bookCode",
                unique: true,
                filter: "[bookCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentDetail_paymentHeaderID",
                table: "TR_PaymentDetail",
                column: "paymentHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentDetailAlloc_paymentDetailID",
                table: "TR_PaymentDetailAlloc",
                column: "paymentDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentHeader_bookingHeaderID",
                table: "TR_PaymentHeader",
                column: "bookingHeaderID");
        }
    }
}
