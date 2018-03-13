using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class add_tb_LKFormulaDP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "formulaDPID",
                table: "TR_BookingDetailDP",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LK_FormulaDP",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    formulaDPDesc = table.Column<string>(maxLength: 100, nullable: false),
                    formulaDPType = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_FormulaDP", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDetailDP_formulaDPID",
                table: "TR_BookingDetailDP",
                column: "formulaDPID");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_BookingDetailDP_LK_FormulaDP_formulaDPID",
                table: "TR_BookingDetailDP",
                column: "formulaDPID",
                principalTable: "LK_FormulaDP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_BookingDetailDP_LK_FormulaDP_formulaDPID",
                table: "TR_BookingDetailDP");

            migrationBuilder.DropTable(
                name: "LK_FormulaDP");

            migrationBuilder.DropIndex(
                name: "IX_TR_BookingDetailDP_formulaDPID",
                table: "TR_BookingDetailDP");

            migrationBuilder.DropColumn(
                name: "formulaDPID",
                table: "TR_BookingDetailDP");
        }
    }
}
