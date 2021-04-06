using Logistics.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual DbSet<DeliveryService> DeliveryServices { get; set; }
        public virtual DbSet<WeightRange> WeightRanges { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
    }
}
