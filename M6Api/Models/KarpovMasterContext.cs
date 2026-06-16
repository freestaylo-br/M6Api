using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace M6Api.Models;

public partial class KarpovMasterContext : DbContext
{
    public KarpovMasterContext()
    {
    }

    public KarpovMasterContext(DbContextOptions<KarpovMasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerProduct> PartnerProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=karpov_master;Username=postgres;Password=root");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.IdMaterialType).HasName("Material_type_pkey");

            entity.ToTable("Material_type");

            entity.Property(e => e.IdMaterialType).HasColumnName("id_material_type");
            entity.Property(e => e.PercentageOfDefective)
                .HasPrecision(5, 2)
                .HasColumnName("percentage_of_defective");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.IdPartner).HasName("Partners_pkey");

            entity.Property(e => e.IdPartner).HasColumnName("id_partner");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.DirectorFirstname)
                .HasMaxLength(50)
                .HasColumnName("director_firstname");
            entity.Property(e => e.DirectorLastname)
                .HasMaxLength(50)
                .HasColumnName("director_lastname");
            entity.Property(e => e.DirectorPatronymic)
                .HasMaxLength(50)
                .HasColumnName("director_patronymic");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.HouseNumber)
                .HasMaxLength(50)
                .HasColumnName("house_number");
            entity.Property(e => e.Index)
                .HasMaxLength(50)
                .HasColumnName("index");
            entity.Property(e => e.Inn)
                .HasMaxLength(50)
                .HasColumnName("inn");
            entity.Property(e => e.PartnerName)
                .HasMaxLength(50)
                .HasColumnName("partner_name");
            entity.Property(e => e.PartnerType)
                .HasMaxLength(50)
                .HasColumnName("partner_type");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phone_number");
            entity.Property(e => e.Rating)
                .HasMaxLength(50)
                .HasColumnName("rating");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .HasColumnName("region");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .HasColumnName("street");
        });

        modelBuilder.Entity<PartnerProduct>(entity =>
        {
            entity.HasKey(e => e.IdPartnerProducts).HasName("Partner_products_pkey");

            entity.ToTable("Partner_products");

            entity.Property(e => e.IdPartnerProducts).HasColumnName("id_partner_products");
            entity.Property(e => e.IdPartner)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_partner");
            entity.Property(e => e.IdProduct)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_product");
            entity.Property(e => e.ProductQuantity)
                .HasMaxLength(50)
                .HasColumnName("product_quantity");
            entity.Property(e => e.SaleDate).HasColumnName("sale_date");

            entity.HasOne(d => d.IdPartnerNavigation).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.IdPartner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_partner");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("Products_pkey");

            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.Article)
                .HasMaxLength(50)
                .HasColumnName("article");
            entity.Property(e => e.IdProductType)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_product_type");
            entity.Property(e => e.MinimumPrice)
                .HasPrecision(16, 2)
                .HasColumnName("minimum_price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasColumnName("product_name");

            entity.HasOne(d => d.IdProductTypeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdProductType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_type");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.IdProductType).HasName("Product_type_pkey");

            entity.ToTable("Product_type");

            entity.Property(e => e.IdProductType).HasColumnName("id_product_type");
            entity.Property(e => e.Coefficient)
                .HasPrecision(10, 2)
                .HasColumnName("coefficient");
            entity.Property(e => e.ProductType1)
                .HasMaxLength(50)
                .HasColumnName("product_type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
