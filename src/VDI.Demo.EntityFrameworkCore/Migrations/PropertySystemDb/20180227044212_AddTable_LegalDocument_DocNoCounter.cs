using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class AddTable_LegalDocument_DocNoCounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocNo_Counter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    coID = table.Column<int>(nullable: false),
                    docID = table.Column<int>(nullable: false),
                    docNo = table.Column<int>(nullable: false),
                    projectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocNo_Counter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocNo_Counter_MS_Company_coID",
                        column: x => x.coID,
                        principalTable: "MS_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocNo_Counter_MS_Document_docID",
                        column: x => x.docID,
                        principalTable: "MS_Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocNo_Counter_MS_Project_projectID",
                        column: x => x.projectID,
                        principalTable: "MS_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocNo_Counter_coID",
                table: "DocNo_Counter",
                column: "coID");

            migrationBuilder.CreateIndex(
                name: "IX_DocNo_Counter_docID",
                table: "DocNo_Counter",
                column: "docID");

            migrationBuilder.CreateIndex(
                name: "IX_DocNo_Counter_projectID",
                table: "DocNo_Counter",
                column: "projectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocNo_Counter");
        }
    }
}
