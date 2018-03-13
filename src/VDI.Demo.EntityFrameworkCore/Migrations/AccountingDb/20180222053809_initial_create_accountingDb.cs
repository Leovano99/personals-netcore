using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.AccountingDb
{
    public partial class initial_create_accountingDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MS_JournalType",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    journalTypeCode = table.Column<string>(maxLength: 11, nullable: false),
                    COACodeFIN = table.Column<string>(maxLength: 5, nullable: false),
                    ACCAlloc = table.Column<short>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    amtTypeCode = table.Column<string>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_JournalType", x => new { x.entityCode, x.journalTypeCode, x.COACodeFIN });
                    table.UniqueConstraint("AK_MS_JournalType_COACodeFIN_entityCode_journalTypeCode", x => new { x.COACodeFIN, x.entityCode, x.journalTypeCode });
                });

            migrationBuilder.CreateTable(
                name: "MS_Mapping",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    journalTypeCode = table.Column<string>(maxLength: 11, nullable: false),
                    othersTypeCode = table.Column<string>(maxLength: 3, nullable: false),
                    payForCode = table.Column<string>(maxLength: 3, nullable: false),
                    payTypeCode = table.Column<string>(maxLength: 3, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Mapping", x => new { x.entityCode, x.journalTypeCode, x.othersTypeCode, x.payForCode, x.payTypeCode });
                });

            migrationBuilder.CreateTable(
                name: "TR_Journal",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    journalCode = table.Column<string>(maxLength: 30, nullable: false),
                    COACodeFIN = table.Column<string>(maxLength: 5, nullable: false),
                    COACodeAcc = table.Column<string>(maxLength: 5, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    debit = table.Column<decimal>(type: "money", nullable: false),
                    journalDate = table.Column<DateTime>(nullable: false),
                    kredit = table.Column<decimal>(type: "money", nullable: false),
                    remarks = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_Journal", x => new { x.entityCode, x.journalCode, x.COACodeFIN, x.COACodeAcc });
                    table.UniqueConstraint("AK_TR_Journal_COACodeAcc_COACodeFIN_entityCode_journalCode", x => new { x.COACodeAcc, x.COACodeFIN, x.entityCode, x.journalCode });
                });

            migrationBuilder.CreateTable(
                name: "TR_PaymentDetailJournal",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    accCode = table.Column<string>(maxLength: 5, nullable: false),
                    transNo = table.Column<string>(maxLength: 20, nullable: false),
                    payNo = table.Column<int>(nullable: false),
                    bookCode = table.Column<string>(maxLength: 20, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    journalCode = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_PaymentDetailJournal", x => new { x.entityCode, x.accCode, x.transNo, x.payNo, x.bookCode });
                    table.UniqueConstraint("AK_TR_PaymentDetailJournal_accCode_bookCode_entityCode_payNo_transNo", x => new { x.accCode, x.bookCode, x.entityCode, x.payNo, x.transNo });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MS_JournalType");

            migrationBuilder.DropTable(
                name: "MS_Mapping");

            migrationBuilder.DropTable(
                name: "TR_Journal");

            migrationBuilder.DropTable(
                name: "TR_PaymentDetailJournal");
        }
    }
}
