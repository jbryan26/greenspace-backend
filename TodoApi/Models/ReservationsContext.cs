using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TodoApi.Auth;
using TodoApi.Models;

namespace TodoApi.Models
{
    public class ReservationsDbContext : DbContext
    {
        public ReservationsDbContext(DbContextOptions<ReservationsDbContext> options) : base(options)
        {
        }

        public DbSet<ReservationModel> ReservationModels { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Field> Fields { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Site> Sites { get; set; }

      //  public DbSet<FormFile> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Id);
            });*/

            //data seed

            //superadmin
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = -2,
                Email = "admin",
                PasswordHash = CryptographyProcessor.Hash("admin"),
                UserRole = UserRoles.SuperAdmin
            });

            modelBuilder.Entity<Field>().HasIndex(field => field.ParentType);

           // modelBuilder.Entity<Field>().HasOne<FieldValue>().WithMany(value => value.FieldId)

            modelBuilder.Entity<Field>()
                .Property(c => c.ParentType)
                .HasConversion<int>();

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<TodoApi.Models.Building> Building { get; set; }


        public DbSet<TodoApi.Models.Floor> Floor { get; set; }
    }
}
