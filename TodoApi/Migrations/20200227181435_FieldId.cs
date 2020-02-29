using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class FieldId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldValue_Fields_FieldId",
                table: "FieldValue");

            migrationBuilder.DropIndex(
                name: "IX_FieldValue_FieldId",
                table: "FieldValue");

            migrationBuilder.AlterColumn<int>(
                name: "FieldId",
                table: "FieldValue",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FieldId1",
                table: "FieldValue",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$TkN6fGPDIO0Xab6Fp2nb+fQp2iQGtcCjn6WA0oDoVTy54B0N");

            migrationBuilder.CreateIndex(
                name: "IX_FieldValue_FieldId1",
                table: "FieldValue",
                column: "FieldId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldValue_Fields_FieldId1",
                table: "FieldValue",
                column: "FieldId1",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldValue_Fields_FieldId1",
                table: "FieldValue");

            migrationBuilder.DropIndex(
                name: "IX_FieldValue_FieldId1",
                table: "FieldValue");

            migrationBuilder.DropColumn(
                name: "FieldId1",
                table: "FieldValue");

            migrationBuilder.AlterColumn<long>(
                name: "FieldId",
                table: "FieldValue",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$2HaHJAUCVOl0Z0hW7Dz5tsqOoVvmM4eYkLisD3zQx0xvsuDv");

            migrationBuilder.CreateIndex(
                name: "IX_FieldValue_FieldId",
                table: "FieldValue",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldValue_Fields_FieldId",
                table: "FieldValue",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
