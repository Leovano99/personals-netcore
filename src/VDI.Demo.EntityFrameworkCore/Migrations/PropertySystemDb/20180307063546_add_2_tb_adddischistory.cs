using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class add_2_tb_adddischistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TR_CommAddDiscHistory",
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
                    table.PrimaryKey("PK_TR_CommAddDiscHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TR_MKTAddDiscHistory",
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
                    table.PrimaryKey("PK_TR_MKTAddDiscHistory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TR_CommAddDiscHistory");

            migrationBuilder.DropTable(
                name: "TR_MKTAddDiscHistory");
        }
    }
}
