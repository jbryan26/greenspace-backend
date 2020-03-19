using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class renameRoommodel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldValue_RoomModels_RoomId",
                table: "FieldValue");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFeaturesItem_RoomModels_RoomId",
                table: "RoomFeaturesItem");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomModels_Floor_FloorId",
                table: "RoomModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomModels",
                table: "RoomModels");

            migrationBuilder.RenameTable(
                name: "RoomModels",
                newName: "Rooms");

            migrationBuilder.RenameIndex(
                name: "IX_RoomModels_FloorId",
                table: "Rooms",
                newName: "IX_Rooms_FloorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$cAmudEA9nop+rovELUfIDC+Kevvhxp/OyxwLeOTc07U7XWYh");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldValue_Rooms_RoomId",
                table: "FieldValue",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFeaturesItem_Rooms_RoomId",
                table: "RoomFeaturesItem",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Floor_FloorId",
                table: "Rooms",
                column: "FloorId",
                principalTable: "Floor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldValue_Rooms_RoomId",
                table: "FieldValue");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFeaturesItem_Rooms_RoomId",
                table: "RoomFeaturesItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Floor_FloorId",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "RoomModels");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_FloorId",
                table: "RoomModels",
                newName: "IX_RoomModels_FloorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomModels",
                table: "RoomModels",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$GT6wc5S64Doa0PSWv6TtqwIqOB6aSFg0IVELoBPISIX2RjMQ");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldValue_RoomModels_RoomId",
                table: "FieldValue",
                column: "RoomId",
                principalTable: "RoomModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFeaturesItem_RoomModels_RoomId",
                table: "RoomFeaturesItem",
                column: "RoomId",
                principalTable: "RoomModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomModels_Floor_FloorId",
                table: "RoomModels",
                column: "FloorId",
                principalTable: "Floor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
