using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class AddIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$fj7UUBuCBFwtj7HvyWdvyDX1fV/XcRA4Iw+WNEppCblgzesm");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_ParentType",
                table: "Fields",
                column: "ParentType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Fields_ParentType",
                table: "Fields");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$dh/Xj4FjsiLQjUGRffVyKZuuD3g+GHBXLfjgeyAgg2xXdxRh");
        }
    }
}
