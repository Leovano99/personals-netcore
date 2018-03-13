using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class add_flagActive_MasterPaymentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "LK_PayType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "LK_PayFor",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "LK_Alloc",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "LK_OthersType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    isActive = table.Column<bool>(nullable: false),
                    isAdjSAD = table.Column<bool>(nullable: false),
                    isOTP = table.Column<bool>(nullable: false),
                    isOthers = table.Column<bool>(nullable: false),
                    isPayment = table.Column<bool>(nullable: false),
                    isSDH = table.Column<bool>(nullable: false),
                    othersTypeCode = table.Column<string>(maxLength: 3, nullable: false),
                    othersTypeDesc = table.Column<string>(maxLength: 50, nullable: false),
                    sortNum = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_OthersType", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LK_OthersType");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "LK_PayType");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "LK_PayFor");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "LK_Alloc");
        }
    }
}
