using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class annualdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Relations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "AnnualDatas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "AnnualDataDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    AnnualDataId = table.Column<int>(type: "int", nullable: false),
                    IsEngineer = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualDataDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnualDataDetails_AnnualDatas_AnnualDataId",
                        column: x => x.AnnualDataId,
                        principalTable: "AnnualDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnualDataDetails_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnualDataDetails_AnnualDataId",
                table: "AnnualDataDetails",
                column: "AnnualDataId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualDataDetails_PersonId",
                table: "AnnualDataDetails",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualDataDetails");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "AnnualDatas");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Relations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
