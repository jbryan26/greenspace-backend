using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationModels",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationDate = table.Column<DateTime>(nullable: false),
                    RoomName = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    ReservationTitle = table.Column<string>(nullable: true),
                    ReservationHost = table.Column<string>(nullable: true),
                    ReservationAttendees = table.Column<int>(nullable: false),
                    ReservationType = table.Column<string>(nullable: true),
                    HasAlcohol = table.Column<bool>(nullable: false),
                    ReservationNotes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomModels",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(nullable: true),
                    ResourceType = table.Column<string>(nullable: true),
                    IsCornerDesk = table.Column<bool>(nullable: false),
                    HasDockingStation = table.Column<bool>(nullable: false),
                    HasDualMonitors = table.Column<bool>(nullable: false),
                    IsFrontDesk = table.Column<bool>(nullable: false),
                    SeatingCapacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodDetailsItem",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(nullable: true),
                    ItemDescription = table.Column<string>(nullable: true),
                    FoodNotes = table.Column<string>(nullable: true),
                    ReservationModelId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodDetailsItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodDetailsItem_ReservationModels_ReservationModelId",
                        column: x => x.ReservationModelId,
                        principalTable: "ReservationModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomFeaturesItem",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureName = table.Column<string>(nullable: true),
                    RoomModelId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomFeaturesItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomFeaturesItem_RoomModels_RoomModelId",
                        column: x => x.RoomModelId,
                        principalTable: "RoomModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodDetailsItem_ReservationModelId",
                table: "FoodDetailsItem",
                column: "ReservationModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomFeaturesItem_RoomModelId",
                table: "RoomFeaturesItem",
                column: "RoomModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodDetailsItem");

            migrationBuilder.DropTable(
                name: "RoomFeaturesItem");

            migrationBuilder.DropTable(
                name: "ReservationModels");

            migrationBuilder.DropTable(
                name: "RoomModels");
        }
    }
}
