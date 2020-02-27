using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class FieldValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DataType",
                table: "Fields",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentType",
                table: "Fields",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FieldValue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldId = table.Column<long>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    LocationId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldValue_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldValue_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$dh/Xj4FjsiLQjUGRffVyKZuuD3g+GHBXLfjgeyAgg2xXdxRh");

            migrationBuilder.CreateIndex(
                name: "IX_FieldValue_FieldId",
                table: "FieldValue",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldValue_LocationId",
                table: "FieldValue",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldValue");

            migrationBuilder.DropColumn(
                name: "ParentType",
                table: "Fields");

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                table: "Fields",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$3mjZ0Opt2jJDB4lrbaCoCEVtJMiJILx+LUAe3xp9P5Fyo1un");
        }
    }
}
