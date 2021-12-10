using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CatalogoTenisWeb.Models
{
    public partial class CatalogoTenisContext : DbContext
    {
        public CatalogoTenisContext()
        {
        }

        public CatalogoTenisContext(DbContextOptions<CatalogoTenisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Tenis> Tenis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4");

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.ToTable("marca");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Tenis>(entity =>
            {
                entity.ToTable("tenis");

                entity.HasIndex(e => e.IdMarca, "fkMarca_idx");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Precio).HasPrecision(19, 4);

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.Tenis)
                    .HasForeignKey(d => d.IdMarca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkMarca");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
