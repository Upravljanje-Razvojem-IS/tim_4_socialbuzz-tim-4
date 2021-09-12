using System;
using Microsoft.EntityFrameworkCore;
using PASMicroservice.Entities;

namespace PASMicroservice.DBContexts
{
    public class PASContext : DbContext
    {
        public PASContext(DbContextOptions<PASContext> options) : base(options) { }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ListingType> ListingTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListingType>().HasMany(t => t.Listings).WithOne(l => l.ListingType);
            modelBuilder.Entity<Category>().HasMany(c => c.Listings).WithOne(l => l.Category);

            // Types
            modelBuilder.Entity<ListingType>().HasData(
                new ListingType
                {
                    ListingTypeId = 1,
                    Name = "product"
                },
                new ListingType
                {
                    ListingTypeId = 2,
                    Name = "service"
                });

            // Categories
            modelBuilder.Entity<Category>().Property(o => o.ParentCategoryId).IsRequired(false);
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = new Guid("329f5f35-9ae7-4bd7-89ff-480cfa938804"),
                    Name = "PC komponente"
                },
                new Category
                {
                    CategoryId = new Guid("dcb3e419-3f9a-4f45-ae1a-df2a57e7eefa"),
                    Name = "Grafičke kartice",
                    ParentCategoryId = new Guid("329f5f35-9ae7-4bd7-89ff-480cfa938804")
                },
                new Category
                {
                    CategoryId = new Guid("c1df5575-00ce-4ca8-88c0-750c9fab1772"),
                    Name = "Usluge | IT"
                },
                new Category
                {
                    CategoryId = new Guid("4c65f2f6-34f0-4440-8a7f-18a617459b7e"),
                    Name = "Usluge | Web development",
                    ParentCategoryId = new Guid("c1df5575-00ce-4ca8-88c0-750c9fab1772")
                });

            // Listing
            modelBuilder.Entity<Listing>().Property(o => o.Price).IsRequired(false).HasDefaultValue((double) 0);
            modelBuilder.Entity<Listing>().Property(o => o.PriceContact).IsRequired(false).HasDefaultValue(false);
            modelBuilder.Entity<Listing>().Property(o => o.PriceDeal).IsRequired(false).HasDefaultValue(false);
            modelBuilder.Entity<Listing>().HasData(
                new Listing
                {
                    ListingId = new Guid("accbc9e4-5705-4683-b30a-40b6e5758a73"),
                    Name = "Sapphire Nitro+ RX 580 8GB",
                    Description = "polovna graficka kartica",
                    Price = 150.00,
                    PriceContact = false,
                    PriceDeal = false,
                    CategoryId = new Guid("dcb3e419-3f9a-4f45-ae1a-df2a57e7eefa"),
                    ListingTypeId = 1,
                    UserId = 1337
                },
                new Listing
                {
                    ListingId = new Guid("a6a07aeb-86bb-4a15-9c70-ce4e439fb965"),
                    Name = "Gigabyte RTX 3080 Ti",
                    Description = "nova graficka kartica",
                    Price = 2499.95,
                    PriceContact = false,
                    PriceDeal = false,
                    CategoryId = new Guid("dcb3e419-3f9a-4f45-ae1a-df2a57e7eefa"),
                    ListingTypeId = 1,
                    UserId = 1338
                },
                new Listing
                {
                    ListingId = new Guid("ae63dcce-07d0-468e-9940-e840ee895aac"),
                    Name = "Cooler Master CPU Hladnjak",
                    PriceDeal = true,
                    CategoryId = new Guid("329f5f35-9ae7-4bd7-89ff-480cfa938804"),
                    ListingTypeId = 1,
                    UserId = 1339
                },
                new Listing
                {
                    ListingId = new Guid("794a9f38-8e36-4f94-901f-76c395727fc5"),
                    Name = "Izrada Wordpress veb sajta",
                    Price = 150.00,
                    PriceDeal = false,
                    PriceContact = false,
                    CategoryId = new Guid("4c65f2f6-34f0-4440-8a7f-18a617459b7e"),
                    ListingTypeId = 2,
                    UserId = 1337
                },
                new Listing
                {
                    ListingId = new Guid("5d6c5c19-d166-41c3-ba84-112a542c4b0c"),
                    Name = "SEO optimizacija",
                    Description = "Za vise informacija, kontaktirati",
                    PriceContact = true,
                    CategoryId = new Guid("c1df5575-00ce-4ca8-88c0-750c9fab1772"),
                    ListingTypeId = 2,
                    UserId = 1337
                }
                );
        }
    }
}
