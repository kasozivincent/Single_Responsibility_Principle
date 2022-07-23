using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.Models
{
    public partial class srpContext : DbContext
    {
        public srpContext()
        {
        }

        public srpContext(DbContextOptions<srpContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Traderecord> Traderecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=srp;user=root;pwd=password", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");

            modelBuilder.Entity<Traderecord>(entity =>
            {
                entity.ToTable("traderecord");

                entity.Property(e => e.Id).HasMaxLength(6);

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.TotalPrice)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UnitPrice)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
