namespace CommentingService.Data
{
    using CommentingService.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    //Potrebno za komunikaciju modela sa bazom

    public class ContextDB : DbContext
    {
        
        private readonly IConfiguration configuration;

        public ContextDB(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Database"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Empty method - rucno sam unela inicijalne podatke u bazu...
        }
    }
}
