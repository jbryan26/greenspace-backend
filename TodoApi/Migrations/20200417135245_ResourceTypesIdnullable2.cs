using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class ResourceTypesIdnullable2 : Migration
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
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$NasiO2SRq3T4E5oaSFhjWbxbwvvo5dNMWSF6to7sMLI6vXXK");

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

            migrationBuilder.AlterColumn<long>(
                name: "ResourceTypeId",
                table: "Rooms",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$+GgTCplrcCf/BYhK579Q/iMbew5M7EonFKt8VRvg3/kwyKCe");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_ResourceTypes_ResourceTypeId",
                table: "Rooms",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
