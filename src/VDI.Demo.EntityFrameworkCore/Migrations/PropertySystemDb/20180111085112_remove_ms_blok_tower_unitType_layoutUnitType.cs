using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class remove_ms_blok_tower_unitType_layoutUnitType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MS_Block");

            migrationBuilder.DropTable(
                name: "MS_LayoutUnitType");

            migrationBuilder.DropTable(
                name: "MS_Tower");

            migrationBuilder.DropTable(
                name: "MS_UnitType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MS_Block",
                columns: table => new
                {
                    blockCode = table.Column<string>(maxLength: 10, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    blockName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Block", x => x.blockCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_Tower",
                columns: table => new
                {
                    towerCode = table.Column<string>(maxLength: 10, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    towerName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Tower", x => x.towerCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_UnitType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    area = table.Column<double>(nullable: true),
                    dueDate = table.Column<DateTime>(nullable: true),
                    isMultiple = table.Column<bool>(nullable: false),
                    jumlahKamarMandi = table.Column<int>(nullable: true),
                    jumlahKamarTidur = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(maxLength: 150, nullable: true),
                    unitTypeCode = table.Column<string>(maxLength: 5, nullable: false),
                    unitTypeName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_UnitType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_LayoutUnitType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    layout = table.Column<string>(maxLength: 300, nullable: true),
                    unitTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_LayoutUnitType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_LayoutUnitType_MS_UnitType_unitTypeID",
                        column: x => x.unitTypeID,
                        principalTable: "MS_UnitType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MS_LayoutUnitType_unitTypeID",
                table: "MS_LayoutUnitType",
                column: "unitTypeID");
        }
    }
}
