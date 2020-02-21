using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class superadminSeed3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1L);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "UserRole" },
                values: new object[] { -2L, "admin", "$RESERVHASH$V1$10000$yztiDYYJxPvdg76AwysQSmKLefIz7Qx4/qr55Hp+8NoXnhxy", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "UserRole" },
                values: new object[] { -1L, "admin", "$RESERVHASH$V1$10000$GAxsDwod63x25TZpjulPwm2IShrGNKA7Jfk4Hwo5E54RDimP", 0 });
        }
    }
}
