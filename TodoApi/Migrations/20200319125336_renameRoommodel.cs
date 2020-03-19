using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class renameRoommodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldValue_RoomModels_RoomModelId",
                table: "FieldValue");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFeaturesItem_RoomModels_RoomModelId",
                table: "RoomFeaturesItem");

            migrationBuilder.DropIndex(
                name: "IX_RoomFeaturesItem_RoomModelId",
                table: "RoomFeaturesItem");

            migrationBuilder.DropIndex(
                name: "IX_FieldValue_RoomModelId",
                table: "FieldValue");

            migrationBuilder.DropColumn(
                name: "RoomModelId",
                table: "RoomFeaturesItem");

            migrationBuilder.DropColumn(
                name: "RoomModelId",
                table: "FieldValue");

            migrationBuilder.AddColumn<long>(
                name: "RoomId",
                table: "RoomFeaturesItem",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RoomId",
                table: "FieldValue",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$GT6wc5S64Doa0PSWv6TtqwIqOB6aSFg0IVELoBPISIX2RjMQ");

            migrationBuilder.CreateIndex(
                name: "IX_RoomFeaturesItem_RoomId",
                table: "RoomFeaturesItem",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldValue_RoomId",
                table: "FieldValue",
                column: "RoomId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldValue_RoomModels_RoomId",
                table: "FieldValue");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFeaturesItem_RoomModels_RoomId",
                table: "RoomFeaturesItem");

            migrationBuilder.DropIndex(
                name: "IX_RoomFeaturesItem_RoomId",
                table: "RoomFeaturesItem");

            migrationBuilder.DropIndex(
                name: "IX_FieldValue_RoomId",
                table: "FieldValue");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "RoomFeaturesItem");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "FieldValue");

            migrationBuilder.AddColumn<long>(
                name: "RoomModelId",
                table: "RoomFeaturesItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RoomModelId",
                table: "FieldValue",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$QKOiuxlxYRMVy9mNMiyfQSq19NqHvvze9rYCFZf219xfzXKf");

            migrationBuilder.CreateIndex(
                name: "IX_RoomFeaturesItem_RoomModelId",
                table: "RoomFeaturesItem",
                column: "RoomModelId");

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
                name: "FK_RoomFeaturesItem_RoomModels_RoomModelId",
                table: "RoomFeaturesItem",
                column: "RoomModelId",
                principalTable: "RoomModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
