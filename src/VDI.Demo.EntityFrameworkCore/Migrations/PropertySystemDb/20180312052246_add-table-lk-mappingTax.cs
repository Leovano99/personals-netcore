using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class addtablelkmappingTax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LK_MappingTax",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    isVAT = table.Column<bool>(nullable: false),
                    othersTypeID = table.Column<int>(nullable: false),
                    payForID = table.Column<int>(nullable: false),
                    payTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_MappingTax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LK_MappingTax_LK_OthersType_othersTypeID",
                        column: x => x.othersTypeID,
                        principalTable: "LK_OthersType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LK_MappingTax_LK_PayFor_payForID",
                        column: x => x.payForID,
                        principalTable: "LK_PayFor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LK_MappingTax_LK_PayType_payTypeID",
                        column: x => x.payTypeID,
                        principalTable: "LK_PayType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LK_MappingTax_othersTypeID",
                table: "LK_MappingTax",
                column: "othersTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LK_MappingTax_payForID",
                table: "LK_MappingTax",
                column: "payForID");

            migrationBuilder.CreateIndex(
                name: "IX_LK_MappingTax_payTypeID",
                table: "LK_MappingTax",
                column: "payTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LK_MappingTax");
        }
    }
}
