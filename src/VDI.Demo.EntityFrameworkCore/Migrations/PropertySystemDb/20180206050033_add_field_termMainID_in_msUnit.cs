using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class add_field_termMainID_in_msUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitReserved_MS_Unit_unitID",
                table: "TR_UnitReserved");

            migrationBuilder.AddColumn<int>(
                name: "termMainID",
                table: "MS_Unit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MS_Unit_termMainID",
                table: "MS_Unit",
                column: "termMainID");

            migrationBuilder.AddForeignKey(
                name: "FK_MS_Unit_MS_TermMain_termMainID",
                table: "MS_Unit",
                column: "termMainID",
                principalTable: "MS_TermMain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitReserved_MS_Unit_unitID",
                table: "TR_UnitReserved",
                column: "unitID",
                principalTable: "MS_Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_Unit_MS_TermMain_termMainID",
                table: "MS_Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitReserved_MS_Unit_unitID",
                table: "TR_UnitReserved");

            migrationBuilder.DropIndex(
                name: "IX_MS_Unit_termMainID",
                table: "MS_Unit");

            migrationBuilder.DropColumn(
                name: "termMainID",
                table: "MS_Unit");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitReserved_MS_Unit_unitID",
                table: "TR_UnitReserved",
                column: "unitID",
                principalTable: "MS_Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
