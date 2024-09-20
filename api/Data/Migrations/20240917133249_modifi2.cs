using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Data.Migrations
{
    public partial class modifi2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_SurgicalProcedures_SurgicalProceduresId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_SurgicalProceduresId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "SurgicalProceduresId",
                table: "Notes");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_SurgicalProcedureId",
                table: "Notes",
                column: "SurgicalProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_SurgicalProcedures_SurgicalProcedureId",
                table: "Notes",
                column: "SurgicalProcedureId",
                principalTable: "SurgicalProcedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_SurgicalProcedures_SurgicalProcedureId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_SurgicalProcedureId",
                table: "Notes");

            migrationBuilder.AddColumn<int>(
                name: "SurgicalProceduresId",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_SurgicalProceduresId",
                table: "Notes",
                column: "SurgicalProceduresId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_SurgicalProcedures_SurgicalProceduresId",
                table: "Notes",
                column: "SurgicalProceduresId",
                principalTable: "SurgicalProcedures",
                principalColumn: "Id");
        }
    }
}
