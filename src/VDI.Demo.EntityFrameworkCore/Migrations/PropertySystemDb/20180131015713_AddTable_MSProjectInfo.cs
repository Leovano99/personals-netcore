using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class AddTable_MSProjectInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TR_ProjectKeyFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    keyFeatures = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_ProjectKeyFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_ProjectKeyFeaturesCollection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    keyFeaturesID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_ProjectKeyFeaturesCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_ProjectKeyFeaturesCollection_TR_ProjectKeyFeatures_keyFeaturesID",
                        column: x => x.keyFeaturesID,
                        principalTable: "TR_ProjectKeyFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_ProjectInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    keyFeaturesCollectionID = table.Column<int>(nullable: false),
                    projectCode = table.Column<string>(maxLength: 5, nullable: false),
                    projectDesc = table.Column<string>(maxLength: 1000, nullable: true),
                    projectDeveloper = table.Column<string>(maxLength: 5, nullable: true),
                    projectMarketingOffice = table.Column<string>(maxLength: 500, nullable: true),
                    projectMarketingPhone = table.Column<string>(maxLength: 50, nullable: true),
                    projectStatus = table.Column<bool>(nullable: false),
                    projectWebsite = table.Column<string>(maxLength: 100, nullable: true),
                    sitePlansImageUrl = table.Column<string>(maxLength: 100, nullable: true),
                    sitePlansLegend = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_ProjectInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_ProjectInfo_MS_ProjectKeyFeaturesCollection_keyFeaturesCollectionID",
                        column: x => x.keyFeaturesCollectionID,
                        principalTable: "MS_ProjectKeyFeaturesCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_ProjectLocation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    latitude = table.Column<decimal>(nullable: false),
                    locationImageURL = table.Column<string>(maxLength: 100, nullable: true),
                    longitude = table.Column<decimal>(nullable: false),
                    projectAddress = table.Column<string>(maxLength: 500, nullable: true),
                    projectInfoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_ProjectLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_ProjectLocation_MS_ProjectInfo_projectInfoID",
                        column: x => x.projectInfoID,
                        principalTable: "MS_ProjectInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_ProjectImageGallery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    imageAlt = table.Column<string>(maxLength: 500, nullable: true),
                    imageStatus = table.Column<bool>(nullable: false),
                    imageURL = table.Column<string>(maxLength: 100, nullable: true),
                    projectInfoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_ProjectImageGallery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_ProjectImageGallery_MS_ProjectInfo_projectInfoID",
                        column: x => x.projectInfoID,
                        principalTable: "MS_ProjectInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MS_ProjectInfo_keyFeaturesCollectionID",
                table: "MS_ProjectInfo",
                column: "keyFeaturesCollectionID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_ProjectKeyFeaturesCollection_keyFeaturesID",
                table: "MS_ProjectKeyFeaturesCollection",
                column: "keyFeaturesID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_ProjectLocation_projectInfoID",
                table: "MS_ProjectLocation",
                column: "projectInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_ProjectImageGallery_projectInfoID",
                table: "TR_ProjectImageGallery",
                column: "projectInfoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MS_ProjectLocation");

            migrationBuilder.DropTable(
                name: "TR_ProjectImageGallery");

            migrationBuilder.DropTable(
                name: "MS_ProjectInfo");

            migrationBuilder.DropTable(
                name: "MS_ProjectKeyFeaturesCollection");

            migrationBuilder.DropTable(
                name: "TR_ProjectKeyFeatures");
        }
    }
}
