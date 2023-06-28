using Microsoft.EntityFrameworkCore;
using Motion.Models.Domain;

namespace Motion.Database
{
    public class MotionDBContext : DbContext
    {
        public MotionDBContext(DbContextOptions options) : base(options) {}
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<DLocation> DLocations { get; set; }
        public DbSet<Request> Requests { get; set; }
    }
}
