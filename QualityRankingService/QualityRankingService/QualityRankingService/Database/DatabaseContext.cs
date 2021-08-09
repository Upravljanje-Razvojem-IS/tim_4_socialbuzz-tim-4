using Microsoft.EntityFrameworkCore;
using QualityRanking.Entities;

namespace QualityRanking.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public virtual DbSet<Ranking> Rankings { get; set; }
    }
}
