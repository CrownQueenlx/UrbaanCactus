using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Urban.WebMVC.Models;

namespace Urban.WebMVC.Data;

public partial class KetoNificentContext : DbContext
{
    public KetoNificentContext()
    {
    }

    public KetoNificentContext(DbContextOptions<KetoNificentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<IngredientEntity> IngredientEntities { get; set; }

    public virtual DbSet<ProductEntity> ProductEntities { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ServingEntity> ServingEntities { get; set; }

    public virtual DbSet<UserEntity> UserEntities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:UrbanCactus");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<IngredientEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingreden__3214EC07C413A14C");

            entity.ToTable("IngredientEntity", "Keto");

            entity.Property(e => e.DefaultMeasurement)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Ncarb)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NCarb");
            entity.Property(e => e.NcarbCt).HasColumnName("NCarbCt");
        });

        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07A4D7E36A");

            entity.ToTable("ProductEntity", "Keto");

            entity.HasIndex(e => e.UserId, "IX_ProductEntity_UserId");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.ProductEntities)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductEntity_UserEntity");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<ServingEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Serving__3214EC073158665D");

            entity.ToTable("ServingEntity", "Keto");

            entity.HasIndex(e => e.IngredientId, "IX_ServingEntity_IngredientId");

            entity.HasIndex(e => e.ProductId, "IX_ServingEntity_ProductId");

            entity.Property(e => e.Measurement)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Ingredient).WithMany(p => p.ServingEntities)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("FK__Serving__Ingrede__3F466844");

            entity.HasOne(d => d.Product).WithMany(p => p.ServingEntities)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Serving__Product__403A8C7D");
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserEnti__1788CC4C272CD311");

            entity.ToTable("UserEntity", "Keto");

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<UserEntity>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
