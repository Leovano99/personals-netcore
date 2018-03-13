using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class add_field_bookingdetailadddisc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "amtAddDisc",
                table: "TR_BookingDetailAddDisc",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "isAmount",
                table: "TR_BookingDetailAddDisc",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amtAddDisc",
                table: "TR_BookingDetailAddDisc");

            migrationBuilder.DropColumn(
                name: "isAmount",
                table: "TR_BookingDetailAddDisc");
        }
    }
}
