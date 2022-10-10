using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Entities.Context
{
    public partial class ApplicationDbContext : DbContext
    {
        public virtual DbSet<GROUP> GROUPS { get; set; }
        public virtual DbSet<USER> USERs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GROUP>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.id).HasColumnType("integer");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasColumnType("text");
            });

            modelBuilder.Entity<USER>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("USER");

                entity.Property(e => e.groups).HasColumnType("text");

                entity.Property(e => e.id).HasColumnType("integer");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasColumnType("text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
