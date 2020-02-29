using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class fieldIdFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$QmjdEkN6tsSPW1fQ/mjxF4+p08P7j6A8Es1711Gvkvaq0P5/");

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "FieldId1",
                table: "FieldValue",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$4KS3HqtW8Q3gjFDbcIe4plsfX1EuYaJAjCiXcroF1bd4t5Mv");

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
    }
}
