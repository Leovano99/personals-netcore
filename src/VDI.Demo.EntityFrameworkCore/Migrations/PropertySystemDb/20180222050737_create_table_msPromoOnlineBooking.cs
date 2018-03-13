using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class create_table_msPromoOnlineBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MS_PromoOnlineBooking",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    isActive = table.Column<bool>(nullable: false),
                    projectID = table.Column<int>(nullable: false),
                    promoAlt = table.Column<string>(maxLength: 500, nullable: false),
                    promoDataType = table.Column<string>(maxLength: 10, nullable: false),
                    promoFile = table.Column<string>(maxLength: 200, nullable: false),
                    targetURL = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_PromoOnlineBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_PromoOnlineBooking_MS_Project_projectID",
                        column: x => x.projectID,
                        principalTable: "MS_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MS_PromoOnlineBooking_projectID",
                table: "MS_PromoOnlineBooking",
                column: "projectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MS_PromoOnlineBooking");
        }
    }
}
