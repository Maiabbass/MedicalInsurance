using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Data.Migrations
{
    public partial class modified13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngineeringeDepars_Specializations_SpecializationId",
                table: "EngineeringeDepars");

            migrationBuilder.AddForeignKey(
                name: "FK_EngineeringeDepars_Specializations_SpecializationId",
                table: "EngineeringeDepars",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngineeringeDepars_Specializations_SpecializationId",
                table: "EngineeringeDepars");

            migrationBuilder.AddForeignKey(
                name: "FK_EngineeringeDepars_Specializations_SpecializationId",
                table: "EngineeringeDepars",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id");
        }
    }
}
