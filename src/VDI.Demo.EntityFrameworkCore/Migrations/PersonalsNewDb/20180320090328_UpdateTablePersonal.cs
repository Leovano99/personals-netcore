using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PersonalsNewDb
{
    public partial class UpdateTablePersonal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PERSONALS_MEMBER",
                table: "PERSONALS_MEMBER");

            migrationBuilder.AddColumn<string>(
                name: "BankBranchName",
                table: "TR_BankAccount",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isAutoDebit",
                table: "TR_BankAccount",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isMain",
                table: "TR_BankAccount",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "province",
                table: "TR_Address",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "spouName",
                table: "PERSONALS_MEMBER",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "parentMemberCode",
                table: "PERSONALS_MEMBER",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "mothName",
                table: "PERSONALS_MEMBER",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PrincName",
                table: "PERSONALS_MEMBER",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PTName",
                table: "PERSONALS_MEMBER",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<long>(
                name: "bankAccountRefID",
                table: "PERSONALS_MEMBER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "parentPSCode",
                table: "PERSONALS",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "mailGroup",
                table: "PERSONALS",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "birthPlace",
                table: "PERSONALS",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NPWP",
                table: "PERSONALS",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PERSONALS_MEMBER",
                table: "PERSONALS_MEMBER",
                columns: new[] { "entityCode", "psCode", "scmCode", "memberCode" });

            migrationBuilder.CreateTable(
                name: "AbpPersistedGrants",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 200, nullable: false),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(maxLength: 50000, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpPersistedGrants", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpPersistedGrants_SubjectId_ClientId_Type",
                table: "AbpPersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpPersistedGrants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PERSONALS_MEMBER",
                table: "PERSONALS_MEMBER");

            migrationBuilder.DropColumn(
                name: "BankBranchName",
                table: "TR_BankAccount");

            migrationBuilder.DropColumn(
                name: "isAutoDebit",
                table: "TR_BankAccount");

            migrationBuilder.DropColumn(
                name: "isMain",
                table: "TR_BankAccount");

            migrationBuilder.DropColumn(
                name: "province",
                table: "TR_Address");

            migrationBuilder.DropColumn(
                name: "bankAccountRefID",
                table: "PERSONALS_MEMBER");

            migrationBuilder.AlterColumn<string>(
                name: "spouName",
                table: "PERSONALS_MEMBER",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "parentMemberCode",
                table: "PERSONALS_MEMBER",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "mothName",
                table: "PERSONALS_MEMBER",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrincName",
                table: "PERSONALS_MEMBER",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PTName",
                table: "PERSONALS_MEMBER",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "parentPSCode",
                table: "PERSONALS",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "mailGroup",
                table: "PERSONALS",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "birthPlace",
                table: "PERSONALS",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NPWP",
                table: "PERSONALS",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PERSONALS_MEMBER",
                table: "PERSONALS_MEMBER",
                columns: new[] { "entityCode", "psCode" });
        }
    }
}
