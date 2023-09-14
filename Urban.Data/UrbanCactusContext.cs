using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Urban.WebMVC.Models;

namespace Urban.WebMVC.Data;

public partial class UrbanCactusContext : DbContext
{
    public UrbanCactusContext()
    {
    }

    public UrbanCactusContext(DbContextOptions<UrbanCactusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BoutiqueEntity> BoutiqueEntities { get; set; }

    public virtual DbSet<ItemEntity> ItemEntities { get; set; }

    public virtual DbSet<ProductEntity> ProductEntities { get; set; }

    public virtual DbSet<ProductTypeEntity> ProductTypeEntities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:UrbanCactus");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoutiqueEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Boutique__3214EC0707F00D65");

            entity.ToTable("BoutiqueEntity", "Admin");

            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.NickName)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.ProductEntityNavigation).WithMany(p => p.BoutiqueEntities)
                .HasForeignKey(d => d.ProductEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Boutique__Produc__52593CB8");
        });

        modelBuilder.Entity<ItemEntity>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ItemEntity", "Admin");

            entity.Property(e => e.Blurb)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.ProductNavigation).WithMany()
                .HasForeignKey(d => d.Product)
                .HasConstraintName("FK__Item__Product__5535A963");
        });

        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07D5191D37");

            entity.ToTable("ProductEntity", "Admin");

            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.ProductEntities)
                .HasForeignKey(d => d.Type)
                .HasConstraintName("FK__Product__Type__4CA06362");
        });

        modelBuilder.Entity<ProductTypeEntity>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__ProductT__516F03B53B6FAF06");

            entity.ToTable("ProductTypeEntity", "Admin");

            entity.Property(e => e.TypeName)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
