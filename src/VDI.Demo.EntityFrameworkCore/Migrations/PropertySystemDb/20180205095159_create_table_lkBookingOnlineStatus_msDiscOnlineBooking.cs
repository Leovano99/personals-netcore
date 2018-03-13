using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class create_table_lkBookingOnlineStatus_msDiscOnlineBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userID",
                table: "TR_UnitReserved");

            migrationBuilder.AddColumn<string>(
                name: "reservedBy",
                table: "TR_UnitReserved",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "LK_BookingOnlineStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    statusType = table.Column<string>(maxLength: 1, nullable: false),
                    statusTypeName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_BookingOnlineStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_DiscOnlineBooking",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    discDesc = table.Column<string>(maxLength: 200, nullable: true),
                    discPct = table.Column<float>(nullable: false),
                    discRef = table.Column<int>(nullable: false),
                    nameLabel = table.Column<string>(maxLength: 300, nullable: true),
                    userType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_DiscOnlineBooking", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LK_BookingOnlineStatus");

            migrationBuilder.DropTable(
                name: "MS_DiscOnlineBooking");

            migrationBuilder.DropColumn(
                name: "reservedBy",
                table: "TR_UnitReserved");

            migrationBuilder.AddColumn<long>(
                name: "userID",
                table: "TR_UnitReserved",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
