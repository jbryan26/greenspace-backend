using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class imageurls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Sites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "RoomModels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Regions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Floor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Building",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$LDLWM4glWv0FRwibzPz6vnTu8fHTaAqJ09qqjNaFrwCmQWU7");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "RoomModels");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Building");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$rkOS3UJX0MZb/c7asZBOavsonMyVXp39X6wzyrIwVqRuTBaj");
        }
    }
}
