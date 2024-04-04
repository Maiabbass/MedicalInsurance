using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Data.Migrations
{
    public partial class modified2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relations_Persons_PersonId",
                table: "Relations");

            migrationBuilder.DropTable(
                name: "TemporaryClaims");

            migrationBuilder.DropIndex(
                name: "IX_Relations_EngineereId",
                table: "Relations");

            migrationBuilder.DropIndex(
                name: "IX_Relations_PersonId",
                table: "Relations");

            migrationBuilder.DropIndex(
                name: "IX_Persons_GenderId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Hospitals_CityId",
                table: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_Engineeres_SpecializationId",
                table: "Engineeres");

            migrationBuilder.DropIndex(
                name: "IX_Engineeres_statusId",
                table: "Engineeres");

            migrationBuilder.DropIndex(
                name: "IX_Engineeres_WorkPlaceId",
                table: "Engineeres");

            migrationBuilder.DropIndex(
                name: "IX_AnnualDatas_EngineeringUnitsId",
                table: "AnnualDatas");

            migrationBuilder.DropIndex(
                name: "IX_AnnualDatas_PayMethodId",
                table: "AnnualDatas");

            migrationBuilder.DropIndex(
                name: "IX_AnnualDatas_WorkPlaceId",
                table: "AnnualDatas");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Relations");

            migrationBuilder.DropColumn(
                name: "Hospital",
                table: "Claims");

            migrationBuilder.AddColumn<int>(
                name: "RelationId",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ApprovedPrice",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AdditionalPrice",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "Claims",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Trust",
                table: "Claims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Relations_EngineereId",
                table: "Relations",
                column: "EngineereId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_GenderId",
                table: "Persons",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_RelationId",
                table: "Persons",
                column: "RelationId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_CityId",
                table: "Hospitals",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Engineeres_SpecializationId",
                table: "Engineeres",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Engineeres_statusId",
                table: "Engineeres",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Engineeres_WorkPlaceId",
                table: "Engineeres",
                column: "WorkPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_HospitalId",
                table: "Claims",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualDatas_EngineeringUnitsId",
                table: "AnnualDatas",
                column: "EngineeringUnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualDatas_PayMethodId",
                table: "AnnualDatas",
                column: "PayMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualDatas_WorkPlaceId",
                table: "AnnualDatas",
                column: "WorkPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Hospitals_HospitalId",
                table: "Claims",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Relations_RelationId",
                table: "Persons",
                column: "RelationId",
                principalTable: "Relations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Hospitals_HospitalId",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Relations_RelationId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Relations_EngineereId",
                table: "Relations");

            migrationBuilder.DropIndex(
                name: "IX_Persons_GenderId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_RelationId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Hospitals_CityId",
                table: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_Engineeres_SpecializationId",
                table: "Engineeres");

            migrationBuilder.DropIndex(
                name: "IX_Engineeres_statusId",
                table: "Engineeres");

            migrationBuilder.DropIndex(
                name: "IX_Engineeres_WorkPlaceId",
                table: "Engineeres");

            migrationBuilder.DropIndex(
                name: "IX_Claims_HospitalId",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_AnnualDatas_EngineeringUnitsId",
                table: "AnnualDatas");

            migrationBuilder.DropIndex(
                name: "IX_AnnualDatas_PayMethodId",
                table: "AnnualDatas");

            migrationBuilder.DropIndex(
                name: "IX_AnnualDatas_WorkPlaceId",
                table: "AnnualDatas");

            migrationBuilder.DropColumn(
                name: "RelationId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "Trust",
                table: "Claims");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Relations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TotalPrice",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedPrice",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalPrice",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "Hospital",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TemporaryClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdditionalPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnduranceRatio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hospital = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SurgicalPro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryClaims", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relations_EngineereId",
                table: "Relations",
                column: "EngineereId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Relations_PersonId",
                table: "Relations",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_GenderId",
                table: "Persons",
                column: "GenderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_CityId",
                table: "Hospitals",
                column: "CityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Engineeres_SpecializationId",
                table: "Engineeres",
                column: "SpecializationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Engineeres_statusId",
                table: "Engineeres",
                column: "statusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Engineeres_WorkPlaceId",
                table: "Engineeres",
                column: "WorkPlaceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnnualDatas_EngineeringUnitsId",
                table: "AnnualDatas",
                column: "EngineeringUnitsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnnualDatas_PayMethodId",
                table: "AnnualDatas",
                column: "PayMethodId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnnualDatas_WorkPlaceId",
                table: "AnnualDatas",
                column: "WorkPlaceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Relations_Persons_PersonId",
                table: "Relations",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
