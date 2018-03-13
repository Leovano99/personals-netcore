using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class alter_3_tb_adddisc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bookNo",
                table: "TR_MKTAddDisc");

            migrationBuilder.DropColumn(
                name: "coCode",
                table: "TR_MKTAddDisc");

            migrationBuilder.DropColumn(
                name: "bookNo",
                table: "TR_CommAddDisc");

            migrationBuilder.DropColumn(
                name: "coCode",
                table: "TR_CommAddDisc");

            migrationBuilder.DropColumn(
                name: "bookNo",
                table: "TR_CashAddDisc");

            migrationBuilder.DropColumn(
                name: "coCode",
                table: "TR_CashAddDisc");

            migrationBuilder.RenameColumn(
                name: "addDisc",
                table: "TR_MKTAddDisc",
                newName: "pctAddDisc");

            migrationBuilder.RenameColumn(
                name: "addDisc",
                table: "TR_CommAddDisc",
                newName: "pctAddDisc");

            migrationBuilder.RenameColumn(
                name: "addDisc",
                table: "TR_CashAddDisc",
                newName: "pctAddDisc");

            migrationBuilder.AlterColumn<short>(
                name: "addDiscNo",
                table: "TR_MKTAddDisc",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<string>(
                name: "addDiscDesc",
                table: "TR_MKTAddDisc",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "amtAddDisc",
                table: "TR_MKTAddDisc",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "isAmount",
                table: "TR_MKTAddDisc",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<short>(
                name: "addDiscNo",
                table: "TR_CommAddDisc",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<string>(
                name: "addDiscDesc",
                table: "TR_CommAddDisc",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "amtAddDisc",
                table: "TR_CommAddDisc",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "isAmount",
                table: "TR_CommAddDisc",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<short>(
                name: "addDiscNo",
                table: "TR_CashAddDisc",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<string>(
                name: "addDiscDesc",
                table: "TR_CashAddDisc",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "amtAddDisc",
                table: "TR_CashAddDisc",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "isAmount",
                table: "TR_CashAddDisc",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amtAddDisc",
                table: "TR_MKTAddDisc");

            migrationBuilder.DropColumn(
                name: "isAmount",
                table: "TR_MKTAddDisc");

            migrationBuilder.DropColumn(
                name: "amtAddDisc",
                table: "TR_CommAddDisc");

            migrationBuilder.DropColumn(
                name: "isAmount",
                table: "TR_CommAddDisc");

            migrationBuilder.DropColumn(
                name: "amtAddDisc",
                table: "TR_CashAddDisc");

            migrationBuilder.DropColumn(
                name: "isAmount",
                table: "TR_CashAddDisc");

            migrationBuilder.RenameColumn(
                name: "pctAddDisc",
                table: "TR_MKTAddDisc",
                newName: "addDisc");

            migrationBuilder.RenameColumn(
                name: "pctAddDisc",
                table: "TR_CommAddDisc",
                newName: "addDisc");

            migrationBuilder.RenameColumn(
                name: "pctAddDisc",
                table: "TR_CashAddDisc",
                newName: "addDisc");

            migrationBuilder.AlterColumn<byte>(
                name: "addDiscNo",
                table: "TR_MKTAddDisc",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<string>(
                name: "addDiscDesc",
                table: "TR_MKTAddDisc",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "bookNo",
                table: "TR_MKTAddDisc",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "coCode",
                table: "TR_MKTAddDisc",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<byte>(
                name: "addDiscNo",
                table: "TR_CommAddDisc",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<string>(
                name: "addDiscDesc",
                table: "TR_CommAddDisc",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "bookNo",
                table: "TR_CommAddDisc",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "coCode",
                table: "TR_CommAddDisc",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<byte>(
                name: "addDiscNo",
                table: "TR_CashAddDisc",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<string>(
                name: "addDiscDesc",
                table: "TR_CashAddDisc",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "bookNo",
                table: "TR_CashAddDisc",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "coCode",
                table: "TR_CashAddDisc",
                maxLength: 5,
                nullable: false,
                defaultValue: "");
        }
    }
}
