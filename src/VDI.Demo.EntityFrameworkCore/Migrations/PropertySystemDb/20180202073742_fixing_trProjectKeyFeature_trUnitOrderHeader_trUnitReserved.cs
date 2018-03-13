using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class fixing_trProjectKeyFeature_trUnitOrderHeader_trUnitReserved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "renovCode",
                table: "TR_UnitReserved");

            migrationBuilder.DropColumn(
                name: "renovName",
                table: "TR_UnitReserved");

            migrationBuilder.DropColumn(
                name: "termName",
                table: "TR_UnitReserved");

            migrationBuilder.DropColumn(
                name: "unitCode",
                table: "TR_UnitReserved");

            migrationBuilder.DropColumn(
                name: "unitNo",
                table: "TR_UnitReserved");

            migrationBuilder.DropColumn(
                name: "Bookcode",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropColumn(
                name: "renovCode",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropColumn(
                name: "renovName",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropColumn(
                name: "termName",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropColumn(
                name: "unitCode",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropColumn(
                name: "unitNo",
                table: "TR_UnitOrderDetail");

            migrationBuilder.RenameColumn(
                name: "termNo",
                table: "TR_UnitReserved",
                newName: "unitID");

            migrationBuilder.RenameColumn(
                name: "termNo",
                table: "TR_UnitOrderDetail",
                newName: "unitID");

            migrationBuilder.AddColumn<int>(
                name: "renovID",
                table: "TR_UnitReserved",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "termID",
                table: "TR_UnitReserved",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "bookingHeaderID",
                table: "TR_UnitOrderDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "renovID",
                table: "TR_UnitOrderDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "termID",
                table: "TR_UnitOrderDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "keyFeatures",
                table: "TR_ProjectKeyFeatures",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitReserved_renovID",
                table: "TR_UnitReserved",
                column: "renovID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitReserved_termID",
                table: "TR_UnitReserved",
                column: "termID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitReserved_unitID",
                table: "TR_UnitReserved",
                column: "unitID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitOrderDetail_bookingHeaderID",
                table: "TR_UnitOrderDetail",
                column: "bookingHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitOrderDetail_renovID",
                table: "TR_UnitOrderDetail",
                column: "renovID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitOrderDetail_termID",
                table: "TR_UnitOrderDetail",
                column: "termID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_UnitOrderDetail_unitID",
                table: "TR_UnitOrderDetail",
                column: "unitID");

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitOrderDetail_TR_BookingHeader_bookingHeaderID",
                table: "TR_UnitOrderDetail",
                column: "bookingHeaderID",
                principalTable: "TR_BookingHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitOrderDetail_MS_Renovation_renovID",
                table: "TR_UnitOrderDetail",
                column: "renovID",
                principalTable: "MS_Renovation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitOrderDetail_MS_Term_termID",
                table: "TR_UnitOrderDetail",
                column: "termID",
                principalTable: "MS_Term",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitOrderDetail_MS_Unit_unitID",
                table: "TR_UnitOrderDetail",
                column: "unitID",
                principalTable: "MS_Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitReserved_MS_Renovation_renovID",
                table: "TR_UnitReserved",
                column: "renovID",
                principalTable: "MS_Renovation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitReserved_MS_Term_termID",
                table: "TR_UnitReserved",
                column: "termID",
                principalTable: "MS_Term",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TR_UnitReserved_MS_Unit_unitID",
                table: "TR_UnitReserved",
                column: "unitID",
                principalTable: "MS_Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitOrderDetail_TR_BookingHeader_bookingHeaderID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitOrderDetail_MS_Renovation_renovID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitOrderDetail_MS_Term_termID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitOrderDetail_MS_Unit_unitID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitReserved_MS_Renovation_renovID",
                table: "TR_UnitReserved");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitReserved_MS_Term_termID",
                table: "TR_UnitReserved");

            migrationBuilder.DropForeignKey(
                name: "FK_TR_UnitReserved_MS_Unit_unitID",
                table: "TR_UnitReserved");

            migrationBuilder.DropIndex(
                name: "IX_TR_UnitReserved_renovID",
                table: "TR_UnitReserved");

            migrationBuilder.DropIndex(
                name: "IX_TR_UnitReserved_termID",
                table: "TR_UnitReserved");

            migrationBuilder.DropIndex(
                name: "IX_TR_UnitReserved_unitID",
                table: "TR_UnitReserved");

            migrationBuilder.DropIndex(
                name: "IX_TR_UnitOrderDetail_bookingHeaderID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_TR_UnitOrderDetail_renovID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_TR_UnitOrderDetail_termID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_TR_UnitOrderDetail_unitID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropColumn(
                name: "renovID",
                table: "TR_UnitReserved");

            migrationBuilder.DropColumn(
                name: "termID",
                table: "TR_UnitReserved");

            migrationBuilder.DropColumn(
                name: "bookingHeaderID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropColumn(
                name: "renovID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.DropColumn(
                name: "termID",
                table: "TR_UnitOrderDetail");

            migrationBuilder.RenameColumn(
                name: "unitID",
                table: "TR_UnitReserved",
                newName: "termNo");

            migrationBuilder.RenameColumn(
                name: "unitID",
                table: "TR_UnitOrderDetail",
                newName: "termNo");

            migrationBuilder.AddColumn<string>(
                name: "renovCode",
                table: "TR_UnitReserved",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "renovName",
                table: "TR_UnitReserved",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "termName",
                table: "TR_UnitReserved",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "unitCode",
                table: "TR_UnitReserved",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "unitNo",
                table: "TR_UnitReserved",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bookcode",
                table: "TR_UnitOrderDetail",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "renovCode",
                table: "TR_UnitOrderDetail",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "renovName",
                table: "TR_UnitOrderDetail",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "termName",
                table: "TR_UnitOrderDetail",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "unitCode",
                table: "TR_UnitOrderDetail",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "unitNo",
                table: "TR_UnitOrderDetail",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "keyFeatures",
                table: "TR_ProjectKeyFeatures",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1000);
        }
    }
}
