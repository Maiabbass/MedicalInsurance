using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Data.Migrations
{
    public partial class modifie1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Engineeres_EngineerId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EngineerId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Engineeres",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "EngineerId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Engineeres_UserId",
                table: "Engineeres",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EngineerId",
                table: "AspNetUsers",
                column: "EngineerId",
                unique: true,
                filter: "[EngineerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Engineeres_EngineerId",
                table: "AspNetUsers",
                column: "EngineerId",
                principalTable: "Engineeres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Engineeres_AspNetUsers_UserId",
                table: "Engineeres",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Engineeres_EngineerId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Engineeres_AspNetUsers_UserId",
                table: "Engineeres");

            migrationBuilder.DropIndex(
                name: "IX_Engineeres_UserId",
                table: "Engineeres");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EngineerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Engineeres");

            migrationBuilder.AlterColumn<int>(
                name: "EngineerId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EngineerId",
                table: "AspNetUsers",
                column: "EngineerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Engineeres_EngineerId",
                table: "AspNetUsers",
                column: "EngineerId",
                principalTable: "Engineeres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
