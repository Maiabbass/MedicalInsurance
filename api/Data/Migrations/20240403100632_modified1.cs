using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Data.Migrations
{
    public partial class modified1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Engineeres_EngineeringUnits_EngineeringUnitsId",
                table: "Engineeres");

            migrationBuilder.DropIndex(
                name: "IX_WorkPlaces_EngineeringUnitsId",
                table: "WorkPlaces");

            migrationBuilder.DropIndex(
                name: "IX_Engineeres_EngineeringUnitsId",
                table: "Engineeres");

            migrationBuilder.DropColumn(
                name: "EngineeringUnitsId",
                table: "Engineeres");

            migrationBuilder.DropColumn(
                name: "CardPrice",
                table: "AgeSegments");

            migrationBuilder.AddColumn<int>(
                name: "EngineereId",
                table: "EngineeringUnits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "AnnualDatas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExAmount",
                table: "AnnualDatas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "AnnualDatas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ToYear",
                table: "AgeSegments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TheAmount",
                table: "AgeSegments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "FromYear",
                table: "AgeSegments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "AgeSegments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlaces_EngineeringUnitsId",
                table: "WorkPlaces",
                column: "EngineeringUnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_EngineeringUnits_EngineereId",
                table: "EngineeringUnits",
                column: "EngineereId");

            migrationBuilder.AddForeignKey(
                name: "FK_EngineeringUnits_Engineeres_EngineereId",
                table: "EngineeringUnits",
                column: "EngineereId",
                principalTable: "Engineeres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngineeringUnits_Engineeres_EngineereId",
                table: "EngineeringUnits");

            migrationBuilder.DropIndex(
                name: "IX_WorkPlaces_EngineeringUnitsId",
                table: "WorkPlaces");

            migrationBuilder.DropIndex(
                name: "IX_EngineeringUnits_EngineereId",
                table: "EngineeringUnits");

            migrationBuilder.DropColumn(
                name: "EngineereId",
                table: "EngineeringUnits");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "AgeSegments");

            migrationBuilder.AddColumn<int>(
                name: "EngineeringUnitsId",
                table: "Engineeres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "AnnualDatas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ExAmount",
                table: "AnnualDatas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Amount",
                table: "AnnualDatas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ToYear",
                table: "AgeSegments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TheAmount",
                table: "AgeSegments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "FromYear",
                table: "AgeSegments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CardPrice",
                table: "AgeSegments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlaces_EngineeringUnitsId",
                table: "WorkPlaces",
                column: "EngineeringUnitsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Engineeres_EngineeringUnitsId",
                table: "Engineeres",
                column: "EngineeringUnitsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Engineeres_EngineeringUnits_EngineeringUnitsId",
                table: "Engineeres",
                column: "EngineeringUnitsId",
                principalTable: "EngineeringUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
