using BillManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Data
{
    public partial class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Factura> Facturi { get; set; }
        public DbSet<DetaliiFactura> DetaliiFacturi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Romanian_CI_AS");

            modelBuilder.Entity<DetaliiFactura>(entity =>
            {
                entity.HasKey(e => new { e.IdDetaliiFactura, e.IdLocatie });

                entity.ToTable("DetaliiFacturi");

                entity.Property(e => e.IdDetaliiFactura).ValueGeneratedOnAdd();

                entity.Property(e => e.Cantitate).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.NumeProdus).IsRequired();

                entity.Property(e => e.PretUnitar).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Valoare).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Factura)
                    .WithMany(p => p.DetaliiFactura)
                    .HasForeignKey(d => new { d.IdFactura, d.IdLocatie })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetaliiFactura_Facturi");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => new { e.IdFactura, e.IdLocatie });

                entity.ToTable("Facturi");

                entity.Property(e => e.IdFactura).ValueGeneratedOnAdd();

                entity.Property(e => e.DataFactura).HasColumnType("date");

                entity.Property(e => e.NumarFactura).IsRequired();

                entity.Property(e => e.NumeClient).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
