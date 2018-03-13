using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class alter_table_msDiscOnlineBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discRef",
                table: "MS_DiscOnlineBooking");

            migrationBuilder.RenameColumn(
                name: "userType",
                table: "MS_DiscOnlineBooking",
                newName: "projectID");

            migrationBuilder.AddColumn<string>(
                name: "batchPP",
                table: "MS_DiscOnlineBooking",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "clusterID",
                table: "MS_DiscOnlineBooking",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "paymentType",
                table: "LK_PaymentType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MS_DiscOnlineBooking_clusterID",
                table: "MS_DiscOnlineBooking",
                column: "clusterID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_DiscOnlineBooking_projectID",
                table: "MS_DiscOnlineBooking",
                column: "projectID");

            migrationBuilder.AddForeignKey(
                name: "FK_MS_DiscOnlineBooking_MS_Cluster_clusterID",
                table: "MS_DiscOnlineBooking",
                column: "clusterID",
                principalTable: "MS_Cluster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MS_DiscOnlineBooking_MS_Project_projectID",
                table: "MS_DiscOnlineBooking",
                column: "projectID",
                principalTable: "MS_Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_DiscOnlineBooking_MS_Cluster_clusterID",
                table: "MS_DiscOnlineBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_MS_DiscOnlineBooking_MS_Project_projectID",
                table: "MS_DiscOnlineBooking");

            migrationBuilder.DropIndex(
                name: "IX_MS_DiscOnlineBooking_clusterID",
                table: "MS_DiscOnlineBooking");

            migrationBuilder.DropIndex(
                name: "IX_MS_DiscOnlineBooking_projectID",
                table: "MS_DiscOnlineBooking");

            migrationBuilder.DropColumn(
                name: "batchPP",
                table: "MS_DiscOnlineBooking");

            migrationBuilder.DropColumn(
                name: "clusterID",
                table: "MS_DiscOnlineBooking");

            migrationBuilder.DropColumn(
                name: "paymentType",
                table: "LK_PaymentType");

            migrationBuilder.RenameColumn(
                name: "projectID",
                table: "MS_DiscOnlineBooking",
                newName: "userType");

            migrationBuilder.AddColumn<int>(
                name: "discRef",
                table: "MS_DiscOnlineBooking",
                nullable: false,
                defaultValue: 0);
        }
    }
}
