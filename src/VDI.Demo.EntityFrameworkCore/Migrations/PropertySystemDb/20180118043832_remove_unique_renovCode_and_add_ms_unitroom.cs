using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class remove_unique_renovCode_and_add_ms_unitroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "renovationCodeUnique",
                table: "MS_Renovation");

            migrationBuilder.DropColumn(
                name: "jumlahKamarMandi",
                table: "MS_UnitItem");

            migrationBuilder.DropColumn(
                name: "jumlahKamarTidur",
                table: "MS_UnitItem");

            migrationBuilder.CreateTable(
                name: "MS_UnitRoom",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bathroom = table.Column<int>(nullable: true),
                    bedroom = table.Column<int>(nullable: true),
                    unitItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_UnitRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_UnitRoom_MS_UnitItem_unitItemID",
                        column: x => x.unitItemID,
                        principalTable: "MS_UnitItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MS_UnitRoom_unitItemID",
                table: "MS_UnitRoom",
                column: "unitItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MS_UnitRoom");

            migrationBuilder.AddColumn<int>(
                name: "jumlahKamarMandi",
                table: "MS_UnitItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "jumlahKamarTidur",
                table: "MS_UnitItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "renovationCodeUnique",
                table: "MS_Renovation",
                column: "renovationCode",
                unique: true);
        }
    }
}
