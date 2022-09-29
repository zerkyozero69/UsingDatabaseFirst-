using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UsingDatabaseFirst.Models.db
{
    public partial class thaivbShopContext : DbContext
    {
        public thaivbShopContext()
        {
        }

        public thaivbShopContext(DbContextOptions<thaivbShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = 192.168.1.10; Database = thaivbShop; User Id = sa; Password = 01012537zz; Integrated Security = False; Trusted_Connection = False; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.ProductTypeId)
                    .HasColumnName("ProductTypeID")
                    .HasMaxLength(2);

                entity.Property(e => e.SerialNumber).HasMaxLength(25);

                entity.Property(e => e.SupplierId)
                    .HasColumnName("SupplierID")
                    .HasMaxLength(3);

                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitID")
                    .HasMaxLength(4);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(x => x.CategoryId)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Product)
                    .HasForeignKey(x => x.ProductTypeId)
                    .HasConstraintName("FK_Product_ProductType");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Product)
                    .HasForeignKey(x => x.SupplierId)
                    .HasConstraintName("FK_Product_Supplier");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Product)
                    .HasForeignKey(x => x.UnitId)
                    .HasConstraintName("FK_Product_Unit");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.Property(e => e.ProductTypeId)
                    .HasColumnName("ProductTypeID")
                    .HasMaxLength(2);

                entity.Property(e => e.ProductTypeName).HasMaxLength(20);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.SupplierId)
                    .HasColumnName("SupplierID")
                    .HasMaxLength(3);

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.ContactName).HasMaxLength(100);

                entity.Property(e => e.SupplierName).HasMaxLength(100);

                entity.Property(e => e.Telephone).HasMaxLength(100);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitID")
                    .HasMaxLength(4);

                entity.Property(e => e.UnitName).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
