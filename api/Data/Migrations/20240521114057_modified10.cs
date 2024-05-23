using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Data.Migrations
{
    public partial class modified10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualDatas_PayMethods_PayMethodId",
                table: "AnnualDatas");

            migrationBuilder.AlterColumn<int>(
                name: "PayMethodId",
                table: "AnnualDatas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualDatas_PayMethods_PayMethodId",
                table: "AnnualDatas",
                column: "PayMethodId",
                principalTable: "PayMethods",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualDatas_PayMethods_PayMethodId",
                table: "AnnualDatas");

            migrationBuilder.AlterColumn<int>(
                name: "PayMethodId",
                table: "AnnualDatas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualDatas_PayMethods_PayMethodId",
                table: "AnnualDatas",
                column: "PayMethodId",
                principalTable: "PayMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
