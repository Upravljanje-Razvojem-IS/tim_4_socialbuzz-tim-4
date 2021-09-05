using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PASMicroservice.Models;

namespace PASMicroservice.DBContexts
{
    public class PASContext : DbContext
    {
        public PASContext(DbContextOptions<PASContext> options) : base(options) { }
        public DbSet<ProductsAndServices> PAS { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PASType>().HasData(
                new PASType
                {
                    Id = 1,
                    Name = "product"
                },
                new PASType
                {
                    Id = 2,
                    Name = "service"
                });
        }
    }
}
