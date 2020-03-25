using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class thumbnails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsThumbnail",
                table: "Image",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PathToFullImage",
                table: "Image",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$ECJ772zxq2k62vOGrZ3QoiGCwCWab0mGM+8Gawyf5mPCOZbn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsThumbnail",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "PathToFullImage",
                table: "Image");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                column: "PasswordHash",
                value: "$RESERVHASH$V1$10000$P9JNsZgA4S2xZf8ciUpPKxj0pvH1/bRzzZLOThYc2nZFmz5e");
        }
    }
}
