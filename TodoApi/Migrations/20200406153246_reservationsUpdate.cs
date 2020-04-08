using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class reservationsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "ReservationModels");

            migrationBuilder.AddColumn<long>(
                name: "RoomId",
                table: "ReservationModels",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$UTB71DCPmZBAsCruBTb5xEdo/H5zjcXMl73WudzxFrMniBW+");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationModels_RoomId",
                table: "ReservationModels",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationModels_Rooms_RoomId",
                table: "ReservationModels",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationModels_Rooms_RoomId",
                table: "ReservationModels");

            migrationBuilder.DropIndex(
                name: "IX_ReservationModels_RoomId",
                table: "ReservationModels");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "ReservationModels");

            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "ReservationModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$LwBfMNz4Mcy01N/m9Ac2veimddx2Rl66WIrOIeLQsYH6usex");
        }
    }
}
