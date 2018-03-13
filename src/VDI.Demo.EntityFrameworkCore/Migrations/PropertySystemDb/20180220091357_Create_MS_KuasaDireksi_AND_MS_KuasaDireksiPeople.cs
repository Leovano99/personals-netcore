using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class Create_MS_KuasaDireksi_AND_MS_KuasaDireksiPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MS_KuasaDireksi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    MS_DocumentPSId = table.Column<int>(nullable: true),
                    docID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    projectID = table.Column<int>(nullable: false),
                    remarks = table.Column<string>(maxLength: 200, nullable: false),
                    suratKuasaImage = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_KuasaDireksi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_KuasaDireksi_MS_Document_MS_DocumentPSId",
                        column: x => x.MS_DocumentPSId,
                        principalTable: "MS_Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MS_KuasaDireksiPeople",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    kuasaDireksiID = table.Column<int>(nullable: false),
                    officerID = table.Column<int>(nullable: true),
                    signeeEmail = table.Column<string>(maxLength: 250, nullable: true),
                    signeeName = table.Column<string>(maxLength: 250, nullable: true),
                    signeePhone = table.Column<string>(maxLength: 20, nullable: true),
                    signeePosition = table.Column<string>(maxLength: 250, nullable: true),
                    signeeSignImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_KuasaDireksiPeople", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_KuasaDireksiPeople_MS_KuasaDireksi_kuasaDireksiID",
                        column: x => x.kuasaDireksiID,
                        principalTable: "MS_KuasaDireksi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MS_KuasaDireksi_MS_DocumentPSId",
                table: "MS_KuasaDireksi",
                column: "MS_DocumentPSId");

            migrationBuilder.CreateIndex(
                name: "IX_MS_KuasaDireksiPeople_kuasaDireksiID",
                table: "MS_KuasaDireksiPeople",
                column: "kuasaDireksiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MS_KuasaDireksiPeople");

            migrationBuilder.DropTable(
                name: "MS_KuasaDireksi");
        }
    }
}
