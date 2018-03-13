using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.NewCommDb
{
    public partial class change_model_builder_has_index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "scmCodeUnique",
                table: "MS_Schema");

            migrationBuilder.CreateIndex(
                name: "scmCodeUnique",
                table: "MS_Schema",
                column: "scmCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "scmCodeUnique",
                table: "MS_Schema");

            migrationBuilder.AddUniqueConstraint(
                name: "scmCodeUnique",
                table: "MS_Schema",
                column: "scmCode");
        }
    }
}
