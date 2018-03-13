using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class alter_table_trBookingHeader_scmCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "schemaID",
                table: "TR_BookingHeader");

            migrationBuilder.AddColumn<string>(
                name: "scmCode",
                table: "TR_BookingHeader",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "scmCode",
                table: "TR_BookingHeader");

            migrationBuilder.AddColumn<int>(
                name: "schemaID",
                table: "TR_BookingHeader",
                nullable: false,
                defaultValue: 0);
        }
    }
}
