using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LoggerService.Entities
{
    public partial class LoggerDBContext : DbContext
    {
        public LoggerDBContext()
        {
        }

        public LoggerDBContext(DbContextOptions<LoggerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Log> Logs { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("LOG_");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ExceptionMessage).IsUnicode(false);

                entity.Property(e => e.ExceptionType).IsUnicode(false);

                entity.Property(e => e.LogLevel).IsUnicode(false);

                entity.Property(e => e.Message)
                    .IsUnicode(false)
                    .HasColumnName("Message_");

                entity.Property(e => e.Microservice).IsUnicode(false);

                entity.Property(e => e.RequestId)
                    .IsUnicode(false)
                    .HasColumnName("RequestID");

                entity.Property(e => e.TimeOfAction).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
