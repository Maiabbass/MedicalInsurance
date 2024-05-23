using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Data.Migrations
{
    public partial class modified12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_Specializations_SpecializationId",
                table: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_Specializations_SpecializationId",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Specializations");

            migrationBuilder.CreateTable(
                name: "EngineeringeDepars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecializationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineeringeDepars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngineeringeDepars_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EngineeringeDepars_SpecializationId",
                table: "EngineeringeDepars",
                column: "SpecializationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EngineeringeDepars");

            migrationBuilder.AddColumn<int>(
                name: "SpecializationId",
                table: "Specializations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_SpecializationId",
                table: "Specializations",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_Specializations_SpecializationId",
                table: "Specializations",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id");
        }
    }
}
