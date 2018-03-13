using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class add_table_sys_rolesOthersType_rolesPayFor_rolesPayType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SYS_RolesOthersType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    othersTypeID = table.Column<int>(nullable: false),
                    rolesID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_RolesOthersType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_RolesOthersType_LK_OthersType_othersTypeID",
                        column: x => x.othersTypeID,
                        principalTable: "LK_OthersType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SYS_RolesPayFor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    payForID = table.Column<int>(nullable: false),
                    rolesID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_RolesPayFor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_RolesPayFor_LK_PayFor_payForID",
                        column: x => x.payForID,
                        principalTable: "LK_PayFor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SYS_RolesPayType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    payTypeID = table.Column<int>(nullable: false),
                    rolesID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_RolesPayType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_RolesPayType_LK_PayType_payTypeID",
                        column: x => x.payTypeID,
                        principalTable: "LK_PayType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SYS_RolesOthersType_othersTypeID",
                table: "SYS_RolesOthersType",
                column: "othersTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_RolesPayFor_payForID",
                table: "SYS_RolesPayFor",
                column: "payForID");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_RolesPayType_payTypeID",
                table: "SYS_RolesPayType",
                column: "payTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SYS_RolesOthersType");

            migrationBuilder.DropTable(
                name: "SYS_RolesPayFor");

            migrationBuilder.DropTable(
                name: "SYS_RolesPayType");
        }
    }
}
