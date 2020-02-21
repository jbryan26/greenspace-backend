﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApi.Models;

namespace TodoApi.Migrations
{
    [DbContext(typeof(ReservationsDbContext))]
    partial class ReservationsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TodoApi.Models.Field", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DataType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormsFilter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupsFilter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("TodoApi.Models.FoodDetailsItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FoodNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<long?>("ReservationModelId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ReservationModelId");

                    b.ToTable("FoodDetailsItem");
                });

            modelBuilder.Entity("TodoApi.Models.Location", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LocationText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("TodoApi.Models.ReservationModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HasAlcohol")
                        .HasColumnType("bit");

                    b.Property<int>("ReservationAttendees")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReservationHost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReservationNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReservationTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReservationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ReservationModels");
                });

            modelBuilder.Entity("TodoApi.Models.RoomFeaturesItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FeatureName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("RoomModelId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoomModelId");

                    b.ToTable("RoomFeaturesItem");
                });

            modelBuilder.Entity("TodoApi.Models.RoomModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("HasDockingStation")
                        .HasColumnType("bit");

                    b.Property<bool>("HasDualMonitors")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCornerDesk")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFrontDesk")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SeatingCapacity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RoomModels");
                });

            modelBuilder.Entity("TodoApi.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = -2L,
                            Email = "admin",
                            PasswordHash = "$RESERVHASH$V1$10000$yztiDYYJxPvdg76AwysQSmKLefIz7Qx4/qr55Hp+8NoXnhxy",
                            UserRole = 2
                        });
                });

            modelBuilder.Entity("TodoApi.Models.FoodDetailsItem", b =>
                {
                    b.HasOne("TodoApi.Models.ReservationModel", null)
                        .WithMany("FoodDetailItems")
                        .HasForeignKey("ReservationModelId");
                });

            modelBuilder.Entity("TodoApi.Models.RoomFeaturesItem", b =>
                {
                    b.HasOne("TodoApi.Models.RoomModel", null)
                        .WithMany("RoomFeatures")
                        .HasForeignKey("RoomModelId");
                });
#pragma warning restore 612, 618
        }
    }
}
