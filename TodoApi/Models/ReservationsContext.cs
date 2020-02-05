using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class ReservationsDbContext : DbContext
    {
        public ReservationsDbContext(DbContextOptions<ReservationsDbContext> options) : base(options)
        {
        }

        public DbSet<ReservationModel> ReservationModels { get; set; }

        public DbSet<RoomModel> RoomModels { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Field> Fields { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Id);
            });*/

            base.OnModelCreating(modelBuilder);
        }
    }
}
