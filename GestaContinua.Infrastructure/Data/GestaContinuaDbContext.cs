using GestaContinua.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaContinua.Infrastructure.Data
{
    public class GestaContinuaDbContext : DbContext
    {
        public GestaContinuaDbContext(DbContextOptions<GestaContinuaDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<ProgressRecord> ProgressRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.TelegramId).IsUnique();
                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<Domain.Entities.Task>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Schedule).HasMaxLength(50);
                entity.Property(e => e.Status).HasMaxLength(50);
                entity.Property(e => e.InputFormat).HasMaxLength(50);
            });

            modelBuilder.Entity<ProgressRecord>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}