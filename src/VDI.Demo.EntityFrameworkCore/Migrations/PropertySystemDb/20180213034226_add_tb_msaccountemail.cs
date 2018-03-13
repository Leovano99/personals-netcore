using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class add_tb_msaccountemail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MS_AccountEmail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    accountID = table.Column<int>(nullable: false),
                    emailCc = table.Column<string>(maxLength: 100, nullable: true),
                    emailTo = table.Column<string>(maxLength: 200, nullable: true),
                    entityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_AccountEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_AccountEmail_MS_Account_accountID",
                        column: x => x.accountID,
                        principalTable: "MS_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MS_AccountEmail_accountID",
                table: "MS_AccountEmail",
                column: "accountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MS_AccountEmail");
        }
    }
}
