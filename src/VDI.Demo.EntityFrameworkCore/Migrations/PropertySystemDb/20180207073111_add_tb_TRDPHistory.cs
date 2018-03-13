using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class add_tb_TRDPHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TR_DPHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DPAmount = table.Column<decimal>(type: "money", nullable: false),
                    DPPct = table.Column<double>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bookingDetailID = table.Column<int>(nullable: false),
                    daysDue = table.Column<short>(nullable: false),
                    dpCalcID = table.Column<int>(nullable: true),
                    dpNo = table.Column<byte>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isSetting = table.Column<bool>(nullable: false),
                    monthsDue = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_DPHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_DPHistory_TR_BookingDetail_bookingDetailID",
                        column: x => x.bookingDetailID,
                        principalTable: "TR_BookingDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TR_DPHistory_bookingDetailID",
                table: "TR_DPHistory",
                column: "bookingDetailID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TR_DPHistory");
        }
    }
}
