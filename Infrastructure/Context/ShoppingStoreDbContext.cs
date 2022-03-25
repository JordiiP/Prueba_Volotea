using System;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Context
{
    public partial class ShoppingStoreDbContext : DbContext
    {
        public ShoppingStoreDbContext()
        {
        }

        public ShoppingStoreDbContext(DbContextOptions<ShoppingStoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Buy> Buy { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ShoppingStoreExamen;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Buy>(entity =>
            {
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(16, 4)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Buy)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Buy_Customer");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Buy)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Buy_Product");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Dni)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(16, 4)");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductTypeId)
                    .HasConstraintName("FK_Product_ProductType");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.Property(e => e.ProductTypeId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });
        }
    }
}
