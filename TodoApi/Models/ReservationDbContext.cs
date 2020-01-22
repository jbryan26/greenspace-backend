using System;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class ReservationDbContext: DbContext
    {
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options): base(options)
        {
        }

        public DbSet<ReservationModel> ReservationModels { get; set; }
    }
}
