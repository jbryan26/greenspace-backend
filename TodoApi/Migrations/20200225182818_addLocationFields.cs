using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class addLocationFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Catering",
                table: "Locations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HaveProjector",
                table: "Locations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfOccupants",
                table: "Locations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$3mjZ0Opt2jJDB4lrbaCoCEVtJMiJILx+LUAe3xp9P5Fyo1un");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Catering",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "HaveProjector",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "NumberOfOccupants",
                table: "Locations");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$yztiDYYJxPvdg76AwysQSmKLefIz7Qx4/qr55Hp+8NoXnhxy");
        }
    }
}
