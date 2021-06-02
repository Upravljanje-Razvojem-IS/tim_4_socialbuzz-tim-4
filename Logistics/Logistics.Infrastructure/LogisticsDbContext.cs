using Logistics.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure
{
    public class LogisticsDbContext : DbContext
    {
        public LogisticsDbContext()
        {
        }

        public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<DistancePrice> DistancePrices { get; set; }
        public virtual DbSet<WeightRange> WeightRanges { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
    }
}
