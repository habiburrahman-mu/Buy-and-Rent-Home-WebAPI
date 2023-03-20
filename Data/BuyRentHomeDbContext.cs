using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BuyandRentHomeWebAPI.Data.Entities;

namespace BuyandRentHomeWebAPI.Data
{
    public partial class BuyRentHomeDbContext : DbContext
    {
        public BuyRentHomeDbContext()
        {
        }

        public BuyRentHomeDbContext(DbContextOptions<BuyRentHomeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<FurnishingType> FurnishingTypes { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyType> PropertyTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserPrivilege> UserPrivileges { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=Default");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LastUpdatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastUpdatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.LastUpdatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.LastUpdatedByNavigation)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.LastUpdatedBy)
                    .HasConstraintName("FK_Countries_Users");
            });

            modelBuilder.Entity<FurnishingType>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasIndex(e => e.PropertyId, "IX_Photos_PropertyId");

                entity.Property(e => e.ImageUrl).IsRequired();

                entity.HasOne(d => d.LastUpdatedByNavigation)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.LastUpdatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.PropertyId);
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasIndex(e => e.FurnishingTypeId, "IX_Properties_FurnishingTypeId");

                entity.HasIndex(e => e.PostedBy, "IX_Properties_PostedBy");

                entity.HasIndex(e => e.PropertyTypeId, "IX_Properties_PropertyTypeId");

                entity.Property(e => e.Landmark).HasMaxLength(50);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.StreetAddress).IsRequired();

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.FurnishingType)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.FurnishingTypeId);

                entity.HasOne(d => d.PostedByNavigation)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.PostedBy);

                entity.HasOne(d => d.PropertyType)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.PropertyTypeId);
            });

            modelBuilder.Entity<PropertyType>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.RoleCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.RoleUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasDefaultValueSql("(N'abc@test.com')");

                entity.Property(e => e.LastUpdatedOn).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasDefaultValueSql("(0x5061737340313233)");

                entity.Property(e => e.Username).IsRequired();
            });

            modelBuilder.Entity<UserPrivilege>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserPrivileges)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPrivileges)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
