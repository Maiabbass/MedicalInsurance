using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class status_engineer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Engineeres_Statuses_StatusId",
                table: "Engineeres");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Engineeres_EngineereId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Engineeres_StatusId",
                table: "Engineeres");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Engineeres");

            migrationBuilder.AlterColumn<int>(
                name: "EngineereId",
                table: "Persons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Engineeres_EngineereId",
                table: "Persons",
                column: "EngineereId",
                principalTable: "Engineeres",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Engineeres_EngineereId",
                table: "Persons");

            migrationBuilder.AlterColumn<int>(
                name: "EngineereId",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Engineeres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Engineeres_StatusId",
                table: "Engineeres",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Engineeres_Statuses_StatusId",
                table: "Engineeres",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Engineeres_EngineereId",
                table: "Persons",
                column: "EngineereId",
                principalTable: "Engineeres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
