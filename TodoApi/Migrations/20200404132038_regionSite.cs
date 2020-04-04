using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class regionSite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$LwBfMNz4Mcy01N/m9Ac2veimddx2Rl66WIrOIeLQsYH6usex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$igo+IGvXsyflw3bn/A8s/Ghm+BqJtc/bdpBsrlk4IWmd4Oug");
        }
    }
}
