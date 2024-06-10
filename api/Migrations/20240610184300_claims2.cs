﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class claims2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_SurgicalProcedures_SurgicalProceduresId",
                table: "Claims");

            migrationBuilder.AlterColumn<int>(
                name: "SurgicalProceduresId",
                table: "Claims",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_SurgicalProcedures_SurgicalProceduresId",
                table: "Claims",
                column: "SurgicalProceduresId",
                principalTable: "SurgicalProcedures",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_SurgicalProcedures_SurgicalProceduresId",
                table: "Claims");

            migrationBuilder.AlterColumn<int>(
                name: "SurgicalProceduresId",
                table: "Claims",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_SurgicalProcedures_SurgicalProceduresId",
                table: "Claims",
                column: "SurgicalProceduresId",
                principalTable: "SurgicalProcedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}