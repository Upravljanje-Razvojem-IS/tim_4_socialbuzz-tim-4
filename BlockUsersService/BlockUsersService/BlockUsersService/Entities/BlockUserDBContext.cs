using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BlockUsersService.Entities
{
    public partial class BlockUserDBContext : DbContext
    {
        public BlockUserDBContext()
        {
        }

        public BlockUserDBContext(DbContextOptions<BlockUserDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Block> Blocks { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Block>(entity =>
            {
                entity.ToTable("BLOCK_");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.BlockDate)
                    .HasColumnType("date")
                    .HasColumnName("block_Date");

                entity.Property(e => e.BlockedId).HasColumnName("blocked_ID");

                entity.Property(e => e.BlockerId).HasColumnName("blocker_ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
