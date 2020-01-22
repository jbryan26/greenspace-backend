using System;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class RoomDbContext: DbContext
    {
        public RoomDbContext(DbContextOptions<RoomDbContext> options) : base(options)
        {
        }

        public DbSet<RoomModel> RoomModels { get; set; }
    }
}
