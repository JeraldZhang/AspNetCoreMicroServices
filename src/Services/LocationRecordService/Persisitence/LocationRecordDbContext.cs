using Microsoft.EntityFrameworkCore;
using LocationService.Models;

namespace LocationService.Persistence
{
    public class LocationRecordDbContext : DbContext
    {
        public LocationRecordDbContext(DbContextOptions<LocationRecordDbContext> options)
            : base(options)
        {
        }

        public DbSet<LocationRecord> LocationRecords { get; set; }
    }
}