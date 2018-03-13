using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class change_sys_financeCounter_to_ai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accCode",
                table: "SYS_FinanceCounter");

            migrationBuilder.DropColumn(
                name: "entityCode",
                table: "SYS_FinanceCounter");

            migrationBuilder.AddColumn<int>(
                name: "accID",
                table: "SYS_FinanceCounter",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "entityID",
                table: "SYS_FinanceCounter",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SYS_FinanceCounter_accID",
                table: "SYS_FinanceCounter",
                column: "accID");

            migrationBuilder.AddForeignKey(
                name: "FK_SYS_FinanceCounter_MS_Account_accID",
                table: "SYS_FinanceCounter",
                column: "accID",
                principalTable: "MS_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SYS_FinanceCounter_MS_Account_accID",
                table: "SYS_FinanceCounter");

            migrationBuilder.DropIndex(
                name: "IX_SYS_FinanceCounter_accID",
                table: "SYS_FinanceCounter");

            migrationBuilder.DropColumn(
                name: "accID",
                table: "SYS_FinanceCounter");

            migrationBuilder.DropColumn(
                name: "entityID",
                table: "SYS_FinanceCounter");

            migrationBuilder.AddColumn<string>(
                name: "accCode",
                table: "SYS_FinanceCounter",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "entityCode",
                table: "SYS_FinanceCounter",
                maxLength: 1,
                nullable: false,
                defaultValue: "");
        }
    }
}
