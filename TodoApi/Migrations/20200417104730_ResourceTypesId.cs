using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class ResourceTypesId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_ResourceTypes_ResourceTypeId",
                table: "Rooms");

            migrationBuilder.AlterColumn<long>(
                name: "ResourceTypeId",
                table: "Rooms",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$rpqYaajmWE6we6XRSZNtFraSPXKiSMhE8/7TgGfm3WQtcsug");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_ResourceTypes_ResourceTypeId",
                table: "Rooms",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_ResourceTypes_ResourceTypeId",
                table: "Rooms");

            migrationBuilder.AlterColumn<long>(
                name: "ResourceTypeId",
                table: "Rooms",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$p30n2cF7ZwWa0KlE81bwEObz+7uDaW8KXSM7+SOeG42SwBfF");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_ResourceTypes_ResourceTypeId",
                table: "Rooms",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
