using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuthService.Models
{
    public class AuthDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public AuthDbContext(DbContextOptions<AuthDbContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AuthDB"));
        }

        public DbSet<AuthModel> AuthModel { get; set; }
    }
}
