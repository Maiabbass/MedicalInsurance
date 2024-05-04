using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Data.Migrations
{
    public partial class modified5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stat2",
                table: "Hospitals",
                newName: "Inside");

            migrationBuilder.RenameColumn(
                name: "Stat",
                table: "Hospitals",
                newName: "Enabled");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Claims",
                newName: "MatherName");

            migrationBuilder.AddColumn<int>(
                name: "EngId",
                table: "Claims",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EngineereeId",
                table: "Claims",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_EngineereeId",
                table: "Claims",
                column: "EngineereeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Engineeres_EngineereeId",
                table: "Claims",
                column: "EngineereeId",
                principalTable: "Engineeres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Engineeres_EngineereeId",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_EngineereeId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "EngId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "EngineereeId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Claims");

            migrationBuilder.RenameColumn(
                name: "Inside",
                table: "Hospitals",
                newName: "Stat2");

            migrationBuilder.RenameColumn(
                name: "Enabled",
                table: "Hospitals",
                newName: "Stat");

            migrationBuilder.RenameColumn(
                name: "MatherName",
                table: "Claims",
                newName: "FullName");
        }
    }
}
