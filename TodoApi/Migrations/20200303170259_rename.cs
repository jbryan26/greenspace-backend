using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "FieldValue");

            migrationBuilder.AddColumn<string>(
                name: "ValueString",
                table: "FieldValue",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$r5g33nPlvZI1ZxZpnBb9Cewp35PHnE3ZPYNv+PHP53Kz/m4u");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueString",
                table: "FieldValue");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "FieldValue",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$aiORDk7JqxC1DgqWttEO8iisk9BaXzxmq29007jV3O+6ra1p");
        }
    }
}
