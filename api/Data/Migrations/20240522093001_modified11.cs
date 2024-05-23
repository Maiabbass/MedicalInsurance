using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Data.Migrations
{
    public partial class modified11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SurgicalName",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "SurgicalType",
                table: "Claims");

            migrationBuilder.RenameColumn(
                name: "AdditionalPrice",
                table: "Claims",
                newName: "non_AddForPerson");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "WorkPlaces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SpecializationId",
                table: "Specializations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Emailpresident",
                table: "EngineeringUnits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Namepresident",
                table: "EngineeringUnits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phonepresident",
                table: "EngineeringUnits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Company_fees",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "EnsuranceNumber",
                table: "Claims",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SurgicalProceduresId",
                table: "Claims",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "non_Add",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "EnduranceRatio",
                table: "AgeSegments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SurgicalProcedures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurgicalProcedures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_SpecializationId",
                table: "Specializations",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_SurgicalProceduresId",
                table: "Claims",
                column: "SurgicalProceduresId");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_SurgicalProcedures_SurgicalProceduresId",
                table: "Claims",
                column: "SurgicalProceduresId",
                principalTable: "SurgicalProcedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_Specializations_SpecializationId",
                table: "Specializations",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_SurgicalProcedures_SurgicalProceduresId",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_Specializations_SpecializationId",
                table: "Specializations");

            migrationBuilder.DropTable(
                name: "SurgicalProcedures");

            migrationBuilder.DropIndex(
                name: "IX_Specializations_SpecializationId",
                table: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_Claims_SurgicalProceduresId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "WorkPlaces");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "Emailpresident",
                table: "EngineeringUnits");

            migrationBuilder.DropColumn(
                name: "Namepresident",
                table: "EngineeringUnits");

            migrationBuilder.DropColumn(
                name: "Phonepresident",
                table: "EngineeringUnits");

            migrationBuilder.DropColumn(
                name: "Company_fees",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "EnsuranceNumber",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "SurgicalProceduresId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "non_Add",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "EnduranceRatio",
                table: "AgeSegments");

            migrationBuilder.RenameColumn(
                name: "non_AddForPerson",
                table: "Claims",
                newName: "AdditionalPrice");

            migrationBuilder.AddColumn<string>(
                name: "SurgicalName",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SurgicalType",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
