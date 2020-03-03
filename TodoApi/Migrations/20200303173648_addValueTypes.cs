using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class addValueTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ValueBool",
                table: "FieldValue",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValueDate",
                table: "FieldValue",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ValueInt",
                table: "FieldValue",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$lK/yuRSlL4W4AecH2REiYtToVYx1spns12UZQWtB0nBhCMZ7");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueBool",
                table: "FieldValue");

            migrationBuilder.DropColumn(
                name: "ValueDate",
                table: "FieldValue");

            migrationBuilder.DropColumn(
                name: "ValueInt",
                table: "FieldValue");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$r5g33nPlvZI1ZxZpnBb9Cewp35PHnE3ZPYNv+PHP53Kz/m4u");
        }
    }
}
