using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class Add_2_tb_history : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TR_BookingDetailAddDiscHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    addDiscDesc = table.Column<string>(maxLength: 500, nullable: false),
                    addDiscNo = table.Column<short>(nullable: false),
                    amtAddDisc = table.Column<decimal>(type: "money", nullable: false),
                    bookingDetailID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    historyNo = table.Column<byte>(nullable: false),
                    isAmount = table.Column<bool>(nullable: false),
                    pctAddDisc = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingDetailAddDiscHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingHeaderHistory",
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
                    SADStatusID = table.Column<int>(nullable: false),
                    bankName = table.Column<string>(maxLength: 50, nullable: false),
                    bankNo = table.Column<string>(maxLength: 30, nullable: false),
                    bankRekeningPemilik = table.Column<string>(maxLength: 50, nullable: true),
                    bookCode = table.Column<string>(maxLength: 20, nullable: false),
                    bookDate = table.Column<DateTime>(nullable: false),
                    cancelDate = table.Column<DateTime>(nullable: true),
                    discBFCalcType = table.Column<string>(maxLength: 1, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    eventID = table.Column<int>(nullable: false),
                    facadeID = table.Column<int>(nullable: true),
                    historyNo = table.Column<byte>(nullable: false),
                    isPenaltyStop = table.Column<bool>(nullable: false),
                    isSK = table.Column<bool>(nullable: true),
                    isSMS = table.Column<bool>(nullable: false),
                    memberCode = table.Column<string>(maxLength: 12, nullable: false),
                    memberName = table.Column<string>(maxLength: 100, nullable: false),
                    netPriceComm = table.Column<decimal>(type: "money", nullable: false),
                    nomorRekeningPemilik = table.Column<string>(maxLength: 50, nullable: true),
                    promotionID = table.Column<int>(nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    remarks = table.Column<string>(maxLength: 1500, nullable: false),
                    scmCode = table.Column<string>(maxLength: 3, nullable: false),
                    shopBusinessID = table.Column<int>(nullable: false),
                    sumberDanaID = table.Column<int>(nullable: true),
                    termID = table.Column<int>(nullable: false),
                    termRemarks = table.Column<string>(maxLength: 200, nullable: false),
                    transID = table.Column<int>(nullable: false),
                    tujuanTransaksiID = table.Column<int>(nullable: true),
                    unitID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingHeaderHistory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TR_BookingDetailAddDiscHistory");

            migrationBuilder.DropTable(
                name: "TR_BookingHeaderHistory");
        }
    }
}
