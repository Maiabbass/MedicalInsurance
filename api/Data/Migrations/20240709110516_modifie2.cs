using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Data.Migrations
{
    public partial class modifie2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AnnualDatas_EngineereId",
                table: "AnnualDatas");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualDatas_EngineereId",
                table: "AnnualDatas",
                column: "EngineereId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnnualDatas_Year",
                table: "AnnualDatas",
                column: "Year",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AnnualDatas_EngineereId",
                table: "AnnualDatas");

            migrationBuilder.DropIndex(
                name: "IX_AnnualDatas_Year",
                table: "AnnualDatas");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualDatas_EngineereId",
                table: "AnnualDatas",
                column: "EngineereId");
        }
    }
}
