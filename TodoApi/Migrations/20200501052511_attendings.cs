using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class attendings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestorName = table.Column<string>(nullable: true),
                    RequestorEmail = table.Column<string>(nullable: true),
                    attendingNumber = table.Column<int>(nullable: false),
                    Catering = table.Column<bool>(nullable: false),
                    RequestedDate = table.Column<DateTime>(nullable: false),
                    Avrequirements = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendings", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$Zn1YYpQuybb0OsN9HoQpxN5KJKQtCYQYpXi1WKm6TrYFZNKt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$QvxWn6NZYgWaPQ+HoSdIwoE4cZo6h7Z0H09Np7wpBacavRFD");
        }
    }
}
