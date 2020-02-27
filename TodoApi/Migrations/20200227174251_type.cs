using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$2HaHJAUCVOl0Z0hW7Dz5tsqOoVvmM4eYkLisD3zQx0xvsuDv");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$fj7UUBuCBFwtj7HvyWdvyDX1fV/XcRA4Iw+WNEppCblgzesm");
        }
    }
}
