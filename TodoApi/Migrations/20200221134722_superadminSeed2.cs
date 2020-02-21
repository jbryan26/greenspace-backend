using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class superadminSeed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$GAxsDwod63x25TZpjulPwm2IShrGNKA7Jfk4Hwo5E54RDimP");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$Ykheemvhimaen5xYAOFQrPxloVmkla1dz2gL29ppN+FN9nUd");
        }
    }
}
