using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class addCustomfieldstoRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Floor_Building_BuildingId",
                table: "Floor");

            migrationBuilder.AlterColumn<long>(
                name: "BuildingId",
                table: "Floor",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RoomModelId",
                table: "FieldValue",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$rkOS3UJX0MZb/c7asZBOavsonMyVXp39X6wzyrIwVqRuTBaj");

            migrationBuilder.CreateIndex(
                name: "IX_FieldValue_RoomModelId",
                table: "FieldValue",
                column: "RoomModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldValue_RoomModels_RoomModelId",
                table: "FieldValue",
                column: "RoomModelId",
                principalTable: "RoomModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Floor_Building_BuildingId",
                table: "Floor",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldValue_RoomModels_RoomModelId",
                table: "FieldValue");

            migrationBuilder.DropForeignKey(
                name: "FK_Floor_Building_BuildingId",
                table: "Floor");

            migrationBuilder.DropIndex(
                name: "IX_FieldValue_RoomModelId",
                table: "FieldValue");

            migrationBuilder.DropColumn(
                name: "RoomModelId",
                table: "FieldValue");

            migrationBuilder.AlterColumn<long>(
                name: "BuildingId",
                table: "Floor",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$szNJdj0DVRTKF/uq2Ja9t+hfbyWB8laRgdmHPkeNXbAjzxQv");

            migrationBuilder.AddForeignKey(
                name: "FK_Floor_Building_BuildingId",
                table: "Floor",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
