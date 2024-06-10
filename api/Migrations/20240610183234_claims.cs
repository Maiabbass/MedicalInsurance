using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class claims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Engineeres_EngineereeId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "EngId",
                table: "Claims");

            migrationBuilder.RenameColumn(
                name: "EngineereeId",
                table: "Claims",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Claims_EngineereeId",
                table: "Claims",
                newName: "IX_Claims_PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Persons_PersonId",
                table: "Claims",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Persons_PersonId",
                table: "Claims");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Claims",
                newName: "EngineereeId");

            migrationBuilder.RenameIndex(
                name: "IX_Claims_PersonId",
                table: "Claims",
                newName: "IX_Claims_EngineereeId");

            migrationBuilder.AddColumn<int>(
                name: "EngId",
                table: "Claims",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Engineeres_EngineereeId",
                table: "Claims",
                column: "EngineereeId",
                principalTable: "Engineeres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
