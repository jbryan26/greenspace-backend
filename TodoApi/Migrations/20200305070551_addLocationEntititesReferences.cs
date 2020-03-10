using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class addLocationEntititesReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Building_Sites_SiteId",
                table: "Building");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomModels_Floor_FloorId",
                table: "RoomModels");

            migrationBuilder.AlterColumn<long>(
                name: "FloorId",
                table: "RoomModels",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SiteId",
                table: "Building",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$szNJdj0DVRTKF/uq2Ja9t+hfbyWB8laRgdmHPkeNXbAjzxQv");

            migrationBuilder.AddForeignKey(
                name: "FK_Building_Sites_SiteId",
                table: "Building",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomModels_Floor_FloorId",
                table: "RoomModels",
                column: "FloorId",
                principalTable: "Floor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Building_Sites_SiteId",
                table: "Building");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomModels_Floor_FloorId",
                table: "RoomModels");

            migrationBuilder.AlterColumn<long>(
                name: "FloorId",
                table: "RoomModels",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "SiteId",
                table: "Building",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$MBCS2I9ngfG4P/vwsdMaxcI1PnNjXq+wBsqEzk49JgThOLu8");

            migrationBuilder.AddForeignKey(
                name: "FK_Building_Sites_SiteId",
                table: "Building",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomModels_Floor_FloorId",
                table: "RoomModels",
                column: "FloorId",
                principalTable: "Floor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
