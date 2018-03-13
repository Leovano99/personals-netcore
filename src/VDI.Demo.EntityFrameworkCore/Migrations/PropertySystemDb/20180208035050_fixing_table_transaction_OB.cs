using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class fixing_table_transaction_OB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_UnitItemPrice_MS_UnitCode_unitCodeID",
                table: "MS_UnitItemPrice");

            migrationBuilder.DropColumn(
                name: "renovCode",
                table: "TR_UnitReserved");

            migrationBuilder.DropColumn(
                name: "renovCode",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropColumn(
                name: "sumberDanaCode",
                table: "TR_BookingHeader");

            migrationBuilder.DropColumn(
                name: "tujuanTransaksiCode",
                table: "TR_BookingHeader");

            migrationBuilder.DropColumn(
                name: "termNo",
                table: "MS_UnitItemPrice");

            migrationBuilder.DropColumn(
                name: "unitNo",
                table: "MS_UnitItemPrice");

            migrationBuilder.RenameColumn(
                name: "unitCodeID",
                table: "MS_UnitItemPrice",
                newName: "unitID");

            migrationBuilder.RenameIndex(
                name: "IX_MS_UnitItemPrice_unitCodeID",
                table: "MS_UnitItemPrice",
                newName: "IX_MS_UnitItemPrice_unitID");

            migrationBuilder.AddColumn<int>(
                name: "renovID",
                table: "TR_UnitReserved",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "renovID",
                table: "TR_UnitOrderDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sumberDanaID",
                table: "TR_BookingHeader",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tujuanTransaksiID",
                table: "TR_BookingHeader",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "termID",
                table: "MS_UnitItemPrice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<byte>(
                name: "DPNo",
                table: "MS_TermDP",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<byte>(
                name: "addDiscNo",
                table: "MS_TermAddDisc",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.CreateTable(
                name: "MS_SumberDana",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    sort = table.Column<int>(nullable: false),
                    sumberDanaCode = table.Column<string>(maxLength: 3, nullable: false),
                    sumberDanaName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_SumberDana", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_TermDiscOnlineBooking",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    discName = table.Column<string>(maxLength: 50, nullable: true),
                    pctDisc = table.Column<double>(nullable: false),
                    termID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_TermDiscOnlineBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_TermDiscOnlineBooking_MS_Term_termID",
                        column: x => x.termID,
                        principalTable: "MS_Term",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_TujuanTransaksi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    sort = table.Column<int>(nullable: false),
                    tujuanTransaksiCode = table.Column<string>(maxLength: 3, nullable: false),
                    tujuanTransaksiName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_TujuanTransaksi", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitReserved_renovID",
                table: "TR_UnitReserved",
                column: "renovID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitOrderDetail_renovID",
                table: "TR_UnitOrderDetail",
                column: "renovID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingHeader_sumberDanaID",
                table: "TR_BookingHeader",
                column: "sumberDanaID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingHeader_tujuanTransaksiID",
                table: "TR_BookingHeader",
                column: "tujuanTransaksiID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_UnitItemPrice_renovID",
                table: "MS_UnitItemPrice",
                column: "renovID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_UnitItemPrice_termID",
                table: "MS_UnitItemPrice",
                column: "termID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_TermDiscOnlineBooking_termID",
                table: "MS_TermDiscOnlineBooking",
                column: "termID");

            migrationBuilder.AddForeignKey(
                name: "FK_MS_UnitItemPrice_MS_Renovation_renovID",
                table: "MS_UnitItemPrice",
                column: "renovID",
                principalTable: "MS_Renovation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MS_UnitItemPrice_MS_Term_termID",
                table: "MS_UnitItemPrice",
                column: "termID",
                principalTable: "MS_Term",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MS_UnitItemPrice_MS_Unit_unitID",
                table: "MS_UnitItemPrice",
                column: "unitID",
                principalTable: "MS_Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_BookingHeader_MS_SumberDana_sumberDanaID",
                table: "TR_BookingHeader",
                column: "sumberDanaID",
                principalTable: "MS_SumberDana",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_BookingHeader_MS_TujuanTransaksi_tujuanTransaksiID",
                table: "TR_BookingHeader",
                column: "tujuanTransaksiID",
                principalTable: "MS_TujuanTransaksi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitOrderDetail_MS_Renovation_renovID",
                table: "TR_UnitOrderDetail",
                column: "renovID",
                principalTable: "MS_Renovation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitReserved_MS_Renovation_renovID",
                table: "TR_UnitReserved",
                column: "renovID",
                principalTable: "MS_Renovation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_UnitItemPrice_MS_Renovation_renovID",
                table: "MS_UnitItemPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_MS_UnitItemPrice_MS_Term_termID",
                table: "MS_UnitItemPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_MS_UnitItemPrice_MS_Unit_unitID",
                table: "MS_UnitItemPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_BookingHeader_MS_SumberDana_sumberDanaID",
                table: "TR_BookingHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_BookingHeader_MS_TujuanTransaksi_tujuanTransaksiID",
                table: "TR_BookingHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitOrderDetail_MS_Renovation_renovID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitReserved_MS_Renovation_renovID",
                table: "TR_UnitReserved");

            migrationBuilder.DropTable(
                name: "MS_SumberDana");

            migrationBuilder.DropTable(
                name: "MS_TermDiscOnlineBooking");

            migrationBuilder.DropTable(
                name: "MS_TujuanTransaksi");

            migrationBuilder.DropIndex(
                name: "IX_TR_UnitReserved_renovID",
                table: "TR_UnitReserved");

            migrationBuilder.DropIndex(
                name: "IX_TR_UnitOrderDetail_renovID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_TR_BookingHeader_sumberDanaID",
                table: "TR_BookingHeader");

            migrationBuilder.DropIndex(
                name: "IX_TR_BookingHeader_tujuanTransaksiID",
                table: "TR_BookingHeader");

            migrationBuilder.DropIndex(
                name: "IX_MS_UnitItemPrice_renovID",
                table: "MS_UnitItemPrice");

            migrationBuilder.DropIndex(
                name: "IX_MS_UnitItemPrice_termID",
                table: "MS_UnitItemPrice");

            migrationBuilder.DropColumn(
                name: "renovID",
                table: "TR_UnitReserved");

            migrationBuilder.DropColumn(
                name: "renovID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropColumn(
                name: "sumberDanaID",
                table: "TR_BookingHeader");

            migrationBuilder.DropColumn(
                name: "tujuanTransaksiID",
                table: "TR_BookingHeader");

            migrationBuilder.DropColumn(
                name: "termID",
                table: "MS_UnitItemPrice");

            migrationBuilder.RenameColumn(
                name: "unitID",
                table: "MS_UnitItemPrice",
                newName: "unitCodeID");

            migrationBuilder.RenameIndex(
                name: "IX_MS_UnitItemPrice_unitID",
                table: "MS_UnitItemPrice",
                newName: "IX_MS_UnitItemPrice_unitCodeID");

            migrationBuilder.AddColumn<string>(
                name: "renovCode",
                table: "TR_UnitReserved",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "renovCode",
                table: "TR_UnitOrderDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sumberDanaCode",
                table: "TR_BookingHeader",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tujuanTransaksiCode",
                table: "TR_BookingHeader",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "termNo",
                table: "MS_UnitItemPrice",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "unitNo",
                table: "MS_UnitItemPrice",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "DPNo",
                table: "MS_TermDP",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<short>(
                name: "addDiscNo",
                table: "MS_TermAddDisc",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AddForeignKey(
                name: "FK_MS_UnitItemPrice_MS_UnitCode_unitCodeID",
                table: "MS_UnitItemPrice",
                column: "unitCodeID",
                principalTable: "MS_UnitCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
