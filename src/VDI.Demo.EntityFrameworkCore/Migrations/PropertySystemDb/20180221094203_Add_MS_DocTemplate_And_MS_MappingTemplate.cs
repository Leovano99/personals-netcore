using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class Add_MS_DocTemplate_And_MS_MappingTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MS_DocTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    docID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: true),
                    isMaster = table.Column<bool>(nullable: true),
                    templateFile = table.Column<string>(nullable: false),
                    templateName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_DocTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_DocTemplate_MS_Document_docID",
                        column: x => x.docID,
                        principalTable: "MS_Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_MappingTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    activeFrom = table.Column<DateTime>(nullable: false),
                    activeTo = table.Column<DateTime>(nullable: true),
                    docID = table.Column<int>(nullable: false),
                    docTemplateID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    projectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_MappingTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_MappingTemplate_MS_DocTemplate_docTemplateID",
                        column: x => x.docTemplateID,
                        principalTable: "MS_DocTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MS_DocTemplate_docID",
                table: "MS_DocTemplate",
                column: "docID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_MappingTemplate_docTemplateID",
                table: "MS_MappingTemplate",
                column: "docTemplateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MS_MappingTemplate");

            migrationBuilder.DropTable(
                name: "MS_DocTemplate");
        }
    }
}
