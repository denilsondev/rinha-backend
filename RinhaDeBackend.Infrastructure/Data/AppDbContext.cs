using Microsoft.EntityFrameworkCore;
using RinhaDeBackend.Domain.Entities;

namespace RinhaDeBackend.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Apelido)
                .IsRequired()
                .HasMaxLength(32);

                entity.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(p => p.Nascimento)
                     .IsRequired();

                entity.OwnsMany(p => p.Stack, navigation =>
                {
                    navigation.Property(s => s.Nome)
                              .IsRequired()
                              .HasMaxLength(32);
                });
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
