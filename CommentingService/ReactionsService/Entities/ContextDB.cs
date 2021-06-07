using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Models
{
    public class ContextDB : DbContext
    {

        private readonly IConfiguration configuration;

        public ContextDB(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<TypeOfReaction> TypeOfReaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Database"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reaction>()
                 .HasData(new
                 {
                     ReactionID = Guid.Parse("8ca02e0f-a565-43d7-b8d1-da0a073118fb"),
                     PostID = 1,
                     TypeOfReactionID = 1,
                     UserID = 1

                 });
        }
    }   
}
