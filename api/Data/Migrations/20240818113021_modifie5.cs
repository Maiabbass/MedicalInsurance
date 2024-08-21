using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Data.Migrations
{
    public partial class modifie5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_passwordEngs_EngineerNumber",
                table: "passwordEngs");

            migrationBuilder.CreateIndex(
                name: "IX_passwordEngs_EngineerNumber",
                table: "passwordEngs",
                column: "EngineerNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_passwordEngs_EngineerNumber",
                table: "passwordEngs");

            migrationBuilder.CreateIndex(
                name: "IX_passwordEngs_EngineerNumber",
                table: "passwordEngs",
                column: "EngineerNumber");
        }
    }
}
