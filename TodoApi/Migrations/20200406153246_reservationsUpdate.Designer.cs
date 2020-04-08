﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApi.Models;

namespace TodoApi.Migrations
{
    [DbContext(typeof(ReservationsDbContext))]
    [Migration("20200406153246_reservationsUpdate")]
    partial class reservationsUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TodoApi.Models.Building", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SiteId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("TodoApi.Models.Field", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DataType")
                        .HasColumnType("int");

                    b.Property<string>("DisplayText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormsFilter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupsFilter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentType");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("TodoApi.Models.FieldValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("FieldId")
                        .HasColumnType("bigint");

                    b.Property<long?>("LocationId")
                        .HasColumnType("bigint");

                    b.Property<long?>("RoomId")
                        .HasColumnType("bigint");

                    b.Property<bool>("ValueBool")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ValueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ValueInt")
                        .HasColumnType("int");

                    b.Property<string>("ValueString")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.HasIndex("LocationId");

                    b.HasIndex("RoomId");

                    b.ToTable("FieldValue");
                });

            modelBuilder.Entity("TodoApi.Models.Floor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("BuildingId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("Floor");
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

            modelBuilder.Entity("TodoApi.Models.Image", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsThumbnail")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PathToFullImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoomId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("TodoApi.Models.Location", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Catering")
                        .HasColumnType("bit");

                    b.Property<bool>("HaveProjector")
                        .HasColumnType("bit");

                    b.Property<string>("LocationText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfOccupants")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("TodoApi.Models.Region", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");
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

                    b.Property<long>("RoomId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("ReservationModels");
                });

            modelBuilder.Entity("TodoApi.Models.Room", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("FloorId")
                        .HasColumnType("bigint");

                    b.Property<bool>("HasDockingStation")
                        .HasColumnType("bit");

                    b.Property<bool>("HasDualMonitors")
                        .HasColumnType("bit");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCornerDesk")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFrontDesk")
                        .HasColumnType("bit");

                    b.Property<int>("ResourceType")
                        .HasColumnType("int");

                    b.Property<string>("RoomName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SeatingCapacity")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("TodoApi.Models.RoomFeaturesItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FeatureName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("RoomId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomFeaturesItem");
                });

            modelBuilder.Entity("TodoApi.Models.Site", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RegionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Sites");
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
                            PasswordHash = "$RESERVHASH$V1$10000$UTB71DCPmZBAsCruBTb5xEdo/H5zjcXMl73WudzxFrMniBW+",
                            UserRole = 2
                        });
                });

            modelBuilder.Entity("TodoApi.Models.Building", b =>
                {
                    b.HasOne("TodoApi.Models.Site", "Site")
                        .WithMany("Buildings")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TodoApi.Models.FieldValue", b =>
                {
                    b.HasOne("TodoApi.Models.Field", "Field")
                        .WithMany()
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TodoApi.Models.Location", null)
                        .WithMany("FieldValues")
                        .HasForeignKey("LocationId");

                    b.HasOne("TodoApi.Models.Room", null)
                        .WithMany("FieldValues")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("TodoApi.Models.Floor", b =>
                {
                    b.HasOne("TodoApi.Models.Building", "Building")
                        .WithMany("Floors")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TodoApi.Models.FoodDetailsItem", b =>
                {
                    b.HasOne("TodoApi.Models.ReservationModel", null)
                        .WithMany("FoodDetailItems")
                        .HasForeignKey("ReservationModelId");
                });

            modelBuilder.Entity("TodoApi.Models.Image", b =>
                {
                    b.HasOne("TodoApi.Models.Room", "Room")
                        .WithMany("Images")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TodoApi.Models.ReservationModel", b =>
                {
                    b.HasOne("TodoApi.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TodoApi.Models.Room", b =>
                {
                    b.HasOne("TodoApi.Models.Floor", "Floor")
                        .WithMany("Rooms")
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TodoApi.Models.RoomFeaturesItem", b =>
                {
                    b.HasOne("TodoApi.Models.Room", null)
                        .WithMany("RoomFeatures")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("TodoApi.Models.Site", b =>
                {
                    b.HasOne("TodoApi.Models.Region", "Region")
                        .WithMany("Sites")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
