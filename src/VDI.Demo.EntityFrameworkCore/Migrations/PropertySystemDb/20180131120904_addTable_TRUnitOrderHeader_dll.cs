using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class addTable_TRUnitOrderHeader_dll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TR_UnitOrderHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    memberCode = table.Column<string>(maxLength: 12, nullable: false),
                    memberName = table.Column<string>(maxLength: 200, nullable: false),
                    oldOrderCode = table.Column<string>(maxLength: 20, nullable: true),
                    orderCode = table.Column<string>(maxLength: 20, nullable: true),
                    orderDate = table.Column<DateTime>(nullable: false),
                    payType = table.Column<int>(nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    psEmail = table.Column<string>(maxLength: 200, nullable: false),
                    psName = table.Column<string>(maxLength: 200, nullable: false),
                    psPhone = table.Column<string>(maxLength: 200, nullable: true),
                    scmCode = table.Column<string>(maxLength: 3, nullable: true),
                    status = table.Column<int>(nullable: false),
                    totalAmt = table.Column<decimal>(type: "money", nullable: false),
                    userRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_UnitOrderHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TR_UnitReserved",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BFAmount = table.Column<decimal>(type: "money", nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    SellingPrice = table.Column<decimal>(type: "money", nullable: false),
                    disc1 = table.Column<double>(nullable: true),
                    disc2 = table.Column<double>(nullable: true),
                    groupBU = table.Column<string>(maxLength: 100, nullable: true),
                    pscode = table.Column<string>(maxLength: 8, nullable: true),
                    releaseDate = table.Column<DateTime>(nullable: true),
                    remarks = table.Column<string>(maxLength: 300, nullable: true),
                    renovCode = table.Column<string>(maxLength: 2, nullable: true),
                    renovName = table.Column<string>(maxLength: 20, nullable: true),
                    reserveDate = table.Column<DateTime>(nullable: false),
                    specialDiscType = table.Column<string>(maxLength: 100, nullable: true),
                    termName = table.Column<string>(maxLength: 200, nullable: true),
                    termNo = table.Column<int>(nullable: false),
                    unitCode = table.Column<string>(maxLength: 20, nullable: true),
                    unitNo = table.Column<string>(maxLength: 8, nullable: true),
                    userRefNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_UnitReserved", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TR_UnitOrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BFAmount = table.Column<decimal>(type: "money", nullable: false),
                    Bookcode = table.Column<string>(maxLength: 20, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PPNo = table.Column<string>(maxLength: 6, nullable: true),
                    disc1 = table.Column<double>(nullable: true),
                    disc2 = table.Column<double>(nullable: true),
                    groupBU = table.Column<string>(maxLength: 100, nullable: true),
                    orderID = table.Column<int>(nullable: false),
                    remarks = table.Column<string>(maxLength: 300, nullable: true),
                    renovCode = table.Column<string>(maxLength: 2, nullable: false),
                    renovName = table.Column<string>(maxLength: 20, nullable: false),
                    sellingPrice = table.Column<decimal>(type: "money", nullable: false),
                    specialDiscType = table.Column<string>(maxLength: 100, nullable: true),
                    termName = table.Column<string>(maxLength: 200, nullable: false),
                    termNo = table.Column<int>(nullable: false),
                    unitCode = table.Column<string>(maxLength: 20, nullable: true),
                    unitNo = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_UnitOrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_UnitOrderDetail_TR_UnitOrderHeader_orderID",
                        column: x => x.orderID,
                        principalTable: "TR_UnitOrderHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitOrderDetail_orderID",
                table: "TR_UnitOrderDetail",
                column: "orderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TR_UnitOrderDetail");

            migrationBuilder.DropTable(
                name: "TR_UnitReserved");

            migrationBuilder.DropTable(
                name: "TR_UnitOrderHeader");
        }
    }
}
