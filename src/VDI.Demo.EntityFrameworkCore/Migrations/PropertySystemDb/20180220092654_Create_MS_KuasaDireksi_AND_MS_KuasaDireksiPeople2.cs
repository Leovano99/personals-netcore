using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class Create_MS_KuasaDireksi_AND_MS_KuasaDireksiPeople2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_KuasaDireksi_MS_Document_MS_DocumentPSId",
                table: "MS_KuasaDireksi");

            migrationBuilder.DropIndex(
                name: "IX_MS_KuasaDireksi_MS_DocumentPSId",
                table: "MS_KuasaDireksi");

            migrationBuilder.DropColumn(
                name: "MS_DocumentPSId",
                table: "MS_KuasaDireksi");

            migrationBuilder.CreateIndex(
                name: "IX_MS_KuasaDireksi_docID",
                table: "MS_KuasaDireksi",
                column: "docID");

            migrationBuilder.AddForeignKey(
                name: "FK_MS_KuasaDireksi_MS_Document_docID",
                table: "MS_KuasaDireksi",
                column: "docID",
                principalTable: "MS_Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_KuasaDireksi_MS_Document_docID",
                table: "MS_KuasaDireksi");

            migrationBuilder.DropIndex(
                name: "IX_MS_KuasaDireksi_docID",
                table: "MS_KuasaDireksi");

            migrationBuilder.AddColumn<int>(
                name: "MS_DocumentPSId",
                table: "MS_KuasaDireksi",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MS_KuasaDireksi_MS_DocumentPSId",
                table: "MS_KuasaDireksi",
                column: "MS_DocumentPSId");

            migrationBuilder.AddForeignKey(
                name: "FK_MS_KuasaDireksi_MS_Document_MS_DocumentPSId",
                table: "MS_KuasaDireksi",
                column: "MS_DocumentPSId",
                principalTable: "MS_Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
