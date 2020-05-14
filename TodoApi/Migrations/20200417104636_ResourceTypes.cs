using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class ResourceTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourceType",
                table: "Rooms");

            migrationBuilder.AddColumn<long>(
                name: "ResourceTypeId",
                table: "Rooms",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ResourceTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Plural = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceTypes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$p30n2cF7ZwWa0KlE81bwEObz+7uDaW8KXSM7+SOeG42SwBfF");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ResourceTypeId",
                table: "Rooms",
                column: "ResourceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_ResourceTypes_ResourceTypeId",
                table: "Rooms",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_ResourceTypes_ResourceTypeId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "ResourceTypes");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_ResourceTypeId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ResourceTypeId",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "ResourceType",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$9irW0AwIIVe135XagkyQgYIdePmjvKZ5LCPIF8OZ+xruc1Mz");
        }
    }
}
