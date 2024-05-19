using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Data.Migrations
{
    public partial class modified7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "EngineeringUnits");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EngineeringUnits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "EngineeringUnits");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "EngineeringUnits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
