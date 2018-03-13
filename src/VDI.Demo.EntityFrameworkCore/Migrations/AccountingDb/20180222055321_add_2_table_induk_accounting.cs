using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.AccountingDb
{
    public partial class add_2_table_induk_accounting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MS_COA",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    accCode = table.Column<string>(maxLength: 5, nullable: false),
                    COACodeFIN = table.Column<string>(maxLength: 5, nullable: false),
                    COACodeAcc = table.Column<string>(maxLength: 5, nullable: false),
                    COAName = table.Column<string>(maxLength: 50, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    remarks = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_COA", x => new { x.entityCode, x.accCode, x.COACodeFIN });
                    table.UniqueConstraint("AK_MS_COA_accCode_COACodeFIN_entityCode", x => new { x.accCode, x.COACodeFIN, x.entityCode });
                });

            migrationBuilder.CreateTable(
                name: "MS_COAFIN",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    COACodeFIN = table.Column<string>(maxLength: 10, nullable: false),
                    COACodeName = table.Column<string>(maxLength: 50, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_COAFIN", x => new { x.entityCode, x.COACodeFIN });
                    table.UniqueConstraint("AK_MS_COAFIN_COACodeFIN_entityCode", x => new { x.COACodeFIN, x.entityCode });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MS_COA");

            migrationBuilder.DropTable(
                name: "MS_COAFIN");
        }
    }
}
