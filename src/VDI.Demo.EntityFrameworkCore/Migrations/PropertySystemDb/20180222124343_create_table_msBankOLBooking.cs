using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class create_table_msBankOLBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sumberDanaID",
                table: "TR_UnitOrderHeader",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tujuanTransaksiID",
                table: "TR_UnitOrderHeader",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MS_BankOLBooking",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bankName = table.Column<string>(maxLength: 100, nullable: false),
                    bankRekName = table.Column<string>(maxLength: 150, nullable: false),
                    bankRekNo = table.Column<string>(maxLength: 100, nullable: false),
                    clusterID = table.Column<int>(nullable: false),
                    projectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_BankOLBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_BankOLBooking_MS_Cluster_clusterID",
                        column: x => x.clusterID,
                        principalTable: "MS_Cluster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_BankOLBooking_MS_Project_projectID",
                        column: x => x.projectID,
                        principalTable: "MS_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitOrderHeader_sumberDanaID",
                table: "TR_UnitOrderHeader",
                column: "sumberDanaID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitOrderHeader_tujuanTransaksiID",
                table: "TR_UnitOrderHeader",
                column: "tujuanTransaksiID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_BankOLBooking_clusterID",
                table: "MS_BankOLBooking",
                column: "clusterID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_BankOLBooking_projectID",
                table: "MS_BankOLBooking",
                column: "projectID");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitOrderHeader_MS_SumberDana_sumberDanaID",
                table: "TR_UnitOrderHeader",
                column: "sumberDanaID",
                principalTable: "MS_SumberDana",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitOrderHeader_MS_TujuanTransaksi_tujuanTransaksiID",
                table: "TR_UnitOrderHeader",
                column: "tujuanTransaksiID",
                principalTable: "MS_TujuanTransaksi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitOrderHeader_MS_SumberDana_sumberDanaID",
                table: "TR_UnitOrderHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitOrderHeader_MS_TujuanTransaksi_tujuanTransaksiID",
                table: "TR_UnitOrderHeader");

            migrationBuilder.DropTable(
                name: "MS_BankOLBooking");

            migrationBuilder.DropIndex(
                name: "IX_TR_UnitOrderHeader_sumberDanaID",
                table: "TR_UnitOrderHeader");

            migrationBuilder.DropIndex(
                name: "IX_TR_UnitOrderHeader_tujuanTransaksiID",
                table: "TR_UnitOrderHeader");

            migrationBuilder.DropColumn(
                name: "sumberDanaID",
                table: "TR_UnitOrderHeader");

            migrationBuilder.DropColumn(
                name: "tujuanTransaksiID",
                table: "TR_UnitOrderHeader");
        }
    }
}
