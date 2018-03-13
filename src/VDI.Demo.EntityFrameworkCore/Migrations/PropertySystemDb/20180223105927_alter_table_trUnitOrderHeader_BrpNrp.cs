using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class alter_table_trUnitOrderHeader_BrpNrp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bankRekeningPemilik",
                table: "TR_UnitOrderHeader",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nomorRekeningPemilik",
                table: "TR_UnitOrderHeader",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bankRekeningPemilik",
                table: "TR_UnitOrderHeader");

            migrationBuilder.DropColumn(
                name: "nomorRekeningPemilik",
                table: "TR_UnitOrderHeader");
        }
    }
}
