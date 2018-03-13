using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class addTableLK_PayFor_Edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "payForName",
                table: "LK_PayFor",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 3);

            migrationBuilder.AddColumn<bool>(
                name: "isSDH",
                table: "LK_PayFor",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "payForCode",
                table: "LK_PayFor",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSDH",
                table: "LK_PayFor");

            migrationBuilder.DropColumn(
                name: "payForCode",
                table: "LK_PayFor");

            migrationBuilder.AlterColumn<string>(
                name: "payForName",
                table: "LK_PayFor",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40);
        }
    }
}
