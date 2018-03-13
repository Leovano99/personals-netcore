using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class FixingFKProjectInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_ProjectKeyFeaturesCollection_TR_ProjectKeyFeatures_keyFeaturesID",
                table: "MS_ProjectKeyFeaturesCollection");

            migrationBuilder.DropIndex(
                name: "IX_MS_ProjectKeyFeaturesCollection_keyFeaturesID",
                table: "MS_ProjectKeyFeaturesCollection");

            migrationBuilder.DropColumn(
                name: "keyFeaturesID",
                table: "MS_ProjectKeyFeaturesCollection");

            migrationBuilder.AddColumn<int>(
                name: "keyFeaturesCollectionID",
                table: "TR_ProjectKeyFeatures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TR_ProjectKeyFeatures_keyFeaturesCollectionID",
                table: "TR_ProjectKeyFeatures",
                column: "keyFeaturesCollectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_ProjectKeyFeatures_MS_ProjectKeyFeaturesCollection_keyFeaturesCollectionID",
                table: "TR_ProjectKeyFeatures",
                column: "keyFeaturesCollectionID",
                principalTable: "MS_ProjectKeyFeaturesCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_ProjectKeyFeatures_MS_ProjectKeyFeaturesCollection_keyFeaturesCollectionID",
                table: "TR_ProjectKeyFeatures");

            migrationBuilder.DropIndex(
                name: "IX_TR_ProjectKeyFeatures_keyFeaturesCollectionID",
                table: "TR_ProjectKeyFeatures");

            migrationBuilder.DropColumn(
                name: "keyFeaturesCollectionID",
                table: "TR_ProjectKeyFeatures");

            migrationBuilder.AddColumn<int>(
                name: "keyFeaturesID",
                table: "MS_ProjectKeyFeaturesCollection",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MS_ProjectKeyFeaturesCollection_keyFeaturesID",
                table: "MS_ProjectKeyFeaturesCollection",
                column: "keyFeaturesID");

            migrationBuilder.AddForeignKey(
                name: "FK_MS_ProjectKeyFeaturesCollection_TR_ProjectKeyFeatures_keyFeaturesID",
                table: "MS_ProjectKeyFeaturesCollection",
                column: "keyFeaturesID",
                principalTable: "TR_ProjectKeyFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
