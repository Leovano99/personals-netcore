using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class rename_table_ms_trans_from : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_BookingHeader_MS_Transform_TR_TransformId",
                table: "TR_BookingHeader");

            migrationBuilder.DropTable(
                name: "MS_Transform");

            migrationBuilder.RenameColumn(
                name: "TR_TransformId",
                table: "TR_BookingHeader",
                newName: "TR_TransfromId");

            migrationBuilder.RenameIndex(
                name: "IX_TR_BookingHeader_TR_TransformId",
                table: "TR_BookingHeader",
                newName: "IX_TR_BookingHeader_TR_TransfromId");

            migrationBuilder.CreateTable(
                name: "MS_TransFrom",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    parentTransCode = table.Column<string>(maxLength: 5, nullable: false),
                    transCode = table.Column<string>(maxLength: 5, nullable: false),
                    transDesc = table.Column<string>(maxLength: 100, nullable: false),
                    transName = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_TransFrom", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "transCodeUnique",
                table: "MS_TransFrom",
                column: "transCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_BookingHeader_MS_TransFrom_TR_TransfromId",
                table: "TR_BookingHeader",
                column: "TR_TransfromId",
                principalTable: "MS_TransFrom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_BookingHeader_MS_TransFrom_TR_TransfromId",
                table: "TR_BookingHeader");

            migrationBuilder.DropTable(
                name: "MS_TransFrom");

            migrationBuilder.RenameColumn(
                name: "TR_TransfromId",
                table: "TR_BookingHeader",
                newName: "TR_TransformId");

            migrationBuilder.RenameIndex(
                name: "IX_TR_BookingHeader_TR_TransfromId",
                table: "TR_BookingHeader",
                newName: "IX_TR_BookingHeader_TR_TransformId");

            migrationBuilder.CreateTable(
                name: "MS_Transform",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    parentTransCode = table.Column<string>(maxLength: 5, nullable: false),
                    transCode = table.Column<string>(maxLength: 5, nullable: false),
                    transDesc = table.Column<string>(maxLength: 100, nullable: false),
                    transName = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Transform", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "transCodeUnique",
                table: "MS_Transform",
                column: "transCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_BookingHeader_MS_Transform_TR_TransformId",
                table: "TR_BookingHeader",
                column: "TR_TransformId",
                principalTable: "MS_Transform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
