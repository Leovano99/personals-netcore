using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class change_model_builder_and_add_lk_rental_status_and_ms_detail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_Unit_MS_TermMain_termMainID",
                table: "MS_Unit");

            migrationBuilder.DropUniqueConstraint(
                name: "bookCodeUnique",
                table: "TR_BookingHeader");

            migrationBuilder.DropUniqueConstraint(
                name: "zoningCodeUnique",
                table: "MS_Zoning");

            migrationBuilder.DropIndex(
                name: "IX_MS_Unit_termMainID",
                table: "MS_Unit");

            migrationBuilder.DropUniqueConstraint(
                name: "renovationCodeUnique",
                table: "MS_Renovation");

            migrationBuilder.DropUniqueConstraint(
                name: "projectCodeUnique",
                table: "MS_Project");

            migrationBuilder.DropUniqueConstraint(
                name: "productCodeUnique",
                table: "MS_Product");

            migrationBuilder.DropUniqueConstraint(
                name: "JKBCodeUnique",
                table: "MS_JenisKantorBank");

            migrationBuilder.DropUniqueConstraint(
                name: "formulaCodeUnique",
                table: "MS_FormulaCode");

            migrationBuilder.DropUniqueConstraint(
                name: "facadeCodeUnique",
                table: "MS_Facade");

            migrationBuilder.DropUniqueConstraint(
                name: "entityCodeUnique",
                table: "MS_Entity");

            migrationBuilder.DropUniqueConstraint(
                name: "departmentCodeUnique",
                table: "MS_Department");

            migrationBuilder.DropUniqueConstraint(
                name: "countryCodeUnique",
                table: "MS_Country");

            migrationBuilder.DropUniqueConstraint(
                name: "coCodeUnique",
                table: "MS_Company");

            migrationBuilder.DropUniqueConstraint(
                name: "clusterCodeUnique",
                table: "MS_Cluster");

            migrationBuilder.DropUniqueConstraint(
                name: "categoryCodeUnique",
                table: "MS_Category");

            migrationBuilder.DropUniqueConstraint(
                name: "bankBranchCodeUnique",
                table: "MS_BankBranch");

            migrationBuilder.DropUniqueConstraint(
                name: "bankCodeUnique",
                table: "MS_Bank");

            migrationBuilder.DropUniqueConstraint(
                name: "areaCodeUnique",
                table: "MS_Area");

            migrationBuilder.DropUniqueConstraint(
                name: "accCodeUnique",
                table: "MS_Account");

            migrationBuilder.DropUniqueConstraint(
                name: "unitStatusCodeUnique",
                table: "LK_UnitStatus");

            migrationBuilder.DropUniqueConstraint(
                name: "itemCodeUnique",
                table: "LK_Item");

            migrationBuilder.DropUniqueConstraint(
                name: "finTypeCodeUnique",
                table: "LK_FinType");

            migrationBuilder.DropUniqueConstraint(
                name: "facingCodeUnique",
                table: "LK_Facing");

            migrationBuilder.DropUniqueConstraint(
                name: "bankLevelCodeUnique",
                table: "LK_BankLevel");

            migrationBuilder.DropColumn(
                name: "termMainID",
                table: "MS_Unit");

            migrationBuilder.AlterColumn<string>(
                name: "bookCode",
                table: "TR_BookingHeader",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "jumlahKamarMandi",
                table: "MS_UnitItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "jumlahKamarTidur",
                table: "MS_UnitItem",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "accCode",
                table: "MS_Account",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5);

            migrationBuilder.CreateTable(
                name: "LK_RentalStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    rentalStatusCode = table.Column<string>(maxLength: 1, nullable: false),
                    rentalStatusName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_RentalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_Detail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    detailCode = table.Column<string>(maxLength: 5, nullable: false),
                    detailName = table.Column<string>(maxLength: 50, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isMultiple = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Detail", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "bookCodeUnique",
                table: "TR_BookingHeader",
                column: "bookCode",
                unique: true,
                filter: "[bookCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "zoningCodeUnique",
                table: "MS_Zoning",
                column: "zoningCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MS_Unit_detailID",
                table: "MS_Unit",
                column: "detailID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Unit_rentalStatusID",
                table: "MS_Unit",
                column: "rentalStatusID");

            migrationBuilder.CreateIndex(
                name: "renovationCodeUnique",
                table: "MS_Renovation",
                column: "renovationCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "projectCodeUnique",
                table: "MS_Project",
                column: "projectCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "productCodeUnique",
                table: "MS_Product",
                column: "productCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "JKBCodeUnique",
                table: "MS_JenisKantorBank",
                column: "JKBCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "formulaCodeUnique",
                table: "MS_FormulaCode",
                column: "formulaCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "facadeCodeUnique",
                table: "MS_Facade",
                column: "facadeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "entityCodeUnique",
                table: "MS_Entity",
                column: "entityCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "departmentCodeUnique",
                table: "MS_Department",
                column: "departmentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "countryCodeUnique",
                table: "MS_Country",
                column: "countryCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "coCodeUnique",
                table: "MS_Company",
                column: "coCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "clusterCodeUnique",
                table: "MS_Cluster",
                column: "clusterCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "categoryCodeUnique",
                table: "MS_Category",
                column: "categoryCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "bankBranchCodeUnique",
                table: "MS_BankBranch",
                column: "bankBranchCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "bankCodeUnique",
                table: "MS_Bank",
                column: "bankCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "areaCodeUnique",
                table: "MS_Area",
                column: "areaCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "accCodeUnique",
                table: "MS_Account",
                column: "accCode",
                unique: true,
                filter: "[accCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "unitStatusCodeUnique",
                table: "LK_UnitStatus",
                column: "unitStatusCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "itemCodeUnique",
                table: "LK_Item",
                column: "itemCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "finTypeCodeUnique",
                table: "LK_FinType",
                column: "finTypeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "facingCodeUnique",
                table: "LK_Facing",
                column: "facingCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "bankLevelCodeUnique",
                table: "LK_BankLevel",
                column: "bankLevelCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "rentalStatusCodeUnique",
                table: "LK_RentalStatus",
                column: "rentalStatusCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MS_Unit_MS_Detail_detailID",
                table: "MS_Unit",
                column: "detailID",
                principalTable: "MS_Detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MS_Unit_LK_RentalStatus_rentalStatusID",
                table: "MS_Unit",
                column: "rentalStatusID",
                principalTable: "LK_RentalStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_Unit_MS_Detail_detailID",
                table: "MS_Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_MS_Unit_LK_RentalStatus_rentalStatusID",
                table: "MS_Unit");

            migrationBuilder.DropTable(
                name: "LK_RentalStatus");

            migrationBuilder.DropTable(
                name: "MS_Detail");

            migrationBuilder.DropIndex(
                name: "bookCodeUnique",
                table: "TR_BookingHeader");

            migrationBuilder.DropIndex(
                name: "zoningCodeUnique",
                table: "MS_Zoning");

            migrationBuilder.DropIndex(
                name: "IX_MS_Unit_detailID",
                table: "MS_Unit");

            migrationBuilder.DropIndex(
                name: "IX_MS_Unit_rentalStatusID",
                table: "MS_Unit");

            migrationBuilder.DropIndex(
                name: "renovationCodeUnique",
                table: "MS_Renovation");

            migrationBuilder.DropIndex(
                name: "projectCodeUnique",
                table: "MS_Project");

            migrationBuilder.DropIndex(
                name: "productCodeUnique",
                table: "MS_Product");

            migrationBuilder.DropIndex(
                name: "JKBCodeUnique",
                table: "MS_JenisKantorBank");

            migrationBuilder.DropIndex(
                name: "formulaCodeUnique",
                table: "MS_FormulaCode");

            migrationBuilder.DropIndex(
                name: "facadeCodeUnique",
                table: "MS_Facade");

            migrationBuilder.DropIndex(
                name: "entityCodeUnique",
                table: "MS_Entity");

            migrationBuilder.DropIndex(
                name: "departmentCodeUnique",
                table: "MS_Department");

            migrationBuilder.DropIndex(
                name: "countryCodeUnique",
                table: "MS_Country");

            migrationBuilder.DropIndex(
                name: "coCodeUnique",
                table: "MS_Company");

            migrationBuilder.DropIndex(
                name: "clusterCodeUnique",
                table: "MS_Cluster");

            migrationBuilder.DropIndex(
                name: "categoryCodeUnique",
                table: "MS_Category");

            migrationBuilder.DropIndex(
                name: "bankBranchCodeUnique",
                table: "MS_BankBranch");

            migrationBuilder.DropIndex(
                name: "bankCodeUnique",
                table: "MS_Bank");

            migrationBuilder.DropIndex(
                name: "areaCodeUnique",
                table: "MS_Area");

            migrationBuilder.DropIndex(
                name: "accCodeUnique",
                table: "MS_Account");

            migrationBuilder.DropIndex(
                name: "unitStatusCodeUnique",
                table: "LK_UnitStatus");

            migrationBuilder.DropIndex(
                name: "itemCodeUnique",
                table: "LK_Item");

            migrationBuilder.DropIndex(
                name: "finTypeCodeUnique",
                table: "LK_FinType");

            migrationBuilder.DropIndex(
                name: "facingCodeUnique",
                table: "LK_Facing");

            migrationBuilder.DropIndex(
                name: "bankLevelCodeUnique",
                table: "LK_BankLevel");

            migrationBuilder.DropColumn(
                name: "jumlahKamarMandi",
                table: "MS_UnitItem");

            migrationBuilder.DropColumn(
                name: "jumlahKamarTidur",
                table: "MS_UnitItem");

            migrationBuilder.AlterColumn<string>(
                name: "bookCode",
                table: "TR_BookingHeader",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "termMainID",
                table: "MS_Unit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "accCode",
                table: "MS_Account",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "bookCodeUnique",
                table: "TR_BookingHeader",
                column: "bookCode");

            migrationBuilder.AddUniqueConstraint(
                name: "zoningCodeUnique",
                table: "MS_Zoning",
                column: "zoningCode");

            migrationBuilder.AddUniqueConstraint(
                name: "renovationCodeUnique",
                table: "MS_Renovation",
                column: "renovationCode");

            migrationBuilder.AddUniqueConstraint(
                name: "projectCodeUnique",
                table: "MS_Project",
                column: "projectCode");

            migrationBuilder.AddUniqueConstraint(
                name: "productCodeUnique",
                table: "MS_Product",
                column: "productCode");

            migrationBuilder.AddUniqueConstraint(
                name: "JKBCodeUnique",
                table: "MS_JenisKantorBank",
                column: "JKBCode");

            migrationBuilder.AddUniqueConstraint(
                name: "formulaCodeUnique",
                table: "MS_FormulaCode",
                column: "formulaCode");

            migrationBuilder.AddUniqueConstraint(
                name: "facadeCodeUnique",
                table: "MS_Facade",
                column: "facadeCode");

            migrationBuilder.AddUniqueConstraint(
                name: "entityCodeUnique",
                table: "MS_Entity",
                column: "entityCode");

            migrationBuilder.AddUniqueConstraint(
                name: "departmentCodeUnique",
                table: "MS_Department",
                column: "departmentCode");

            migrationBuilder.AddUniqueConstraint(
                name: "countryCodeUnique",
                table: "MS_Country",
                column: "countryCode");

            migrationBuilder.AddUniqueConstraint(
                name: "coCodeUnique",
                table: "MS_Company",
                column: "coCode");

            migrationBuilder.AddUniqueConstraint(
                name: "clusterCodeUnique",
                table: "MS_Cluster",
                column: "clusterCode");

            migrationBuilder.AddUniqueConstraint(
                name: "categoryCodeUnique",
                table: "MS_Category",
                column: "categoryCode");

            migrationBuilder.AddUniqueConstraint(
                name: "bankBranchCodeUnique",
                table: "MS_BankBranch",
                column: "bankBranchCode");

            migrationBuilder.AddUniqueConstraint(
                name: "bankCodeUnique",
                table: "MS_Bank",
                column: "bankCode");

            migrationBuilder.AddUniqueConstraint(
                name: "areaCodeUnique",
                table: "MS_Area",
                column: "areaCode");

            migrationBuilder.AddUniqueConstraint(
                name: "accCodeUnique",
                table: "MS_Account",
                column: "accCode");

            migrationBuilder.AddUniqueConstraint(
                name: "unitStatusCodeUnique",
                table: "LK_UnitStatus",
                column: "unitStatusCode");

            migrationBuilder.AddUniqueConstraint(
                name: "itemCodeUnique",
                table: "LK_Item",
                column: "itemCode");

            migrationBuilder.AddUniqueConstraint(
                name: "finTypeCodeUnique",
                table: "LK_FinType",
                column: "finTypeCode");

            migrationBuilder.AddUniqueConstraint(
                name: "facingCodeUnique",
                table: "LK_Facing",
                column: "facingCode");

            migrationBuilder.AddUniqueConstraint(
                name: "bankLevelCodeUnique",
                table: "LK_BankLevel",
                column: "bankLevelCode");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Unit_termMainID",
                table: "MS_Unit",
                column: "termMainID");

            migrationBuilder.AddForeignKey(
                name: "FK_MS_Unit_MS_TermMain_termMainID",
                table: "MS_Unit",
                column: "termMainID",
                principalTable: "MS_TermMain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
