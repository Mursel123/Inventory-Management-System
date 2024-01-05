﻿// <auto-generated />
using System;
using InventoryManagementSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryManagementSystem.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240105174939_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IngredientProduct", b =>
                {
                    b.Property<Guid>("IngredientsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IngredientsId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("IngredientProduct");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("FileData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("MlTotal")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MlUsage")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal>("TotalCost")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId")
                        .IsUnique()
                        .HasFilter("[DocumentId] IS NOT NULL");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.OrderLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IngredientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("SubProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalCost")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SubProductId");

                    b.ToTable("OrderLine");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.Price", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IngredientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("IngredientPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("Ml")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("WebsiteLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.ToTable("Price");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("DocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId")
                        .IsUnique()
                        .HasFilter("[DocumentId] IS NOT NULL");

                    b.HasIndex("SupplierId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.ProductType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("ProductType");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8c0a31f9-fc31-4340-9dea-ea47bfe36cea"),
                            IsDeleted = false,
                            Type = "Purchased Inventory"
                        },
                        new
                        {
                            Id = new Guid("bc98af94-2501-42ec-a232-d7d08f38676f"),
                            IsDeleted = false,
                            Type = "Sales Inventory"
                        });
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.Settings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AtLeastIngredientMLTotal")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("AtLeastProductAmount")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Settings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("20f3e671-1e7d-424a-91d8-b5706bb8e269"),
                            AtLeastIngredientMLTotal = 0m,
                            AtLeastProductAmount = 0,
                            IsDeleted = false
                        });
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.SubProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("DocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId")
                        .IsUnique()
                        .HasFilter("[DocumentId] IS NOT NULL");

                    b.HasIndex("SupplierId");

                    b.ToTable("SubProduct");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("ProductProductType", b =>
                {
                    b.Property<Guid>("ProductTypesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductTypesId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("ProductProductType");
                });

            modelBuilder.Entity("ProductSubProduct", b =>
                {
                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubProductsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductsId", "SubProductsId");

                    b.HasIndex("SubProductsId");

                    b.ToTable("ProductSubProduct");
                });

            modelBuilder.Entity("IngredientProduct", b =>
                {
                    b.HasOne("InventoryManagementSystem.Domain.Entities.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryManagementSystem.Domain.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.Order", b =>
                {
                    b.HasOne("InventoryManagementSystem.Domain.Entities.Document", "Document")
                        .WithOne("Order")
                        .HasForeignKey("InventoryManagementSystem.Domain.Entities.Order", "DocumentId");

                    b.Navigation("Document");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.OrderLine", b =>
                {
                    b.HasOne("InventoryManagementSystem.Domain.Entities.Ingredient", "Ingredient")
                        .WithMany("OrderLines")
                        .HasForeignKey("IngredientId");

                    b.HasOne("InventoryManagementSystem.Domain.Entities.Order", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId");

                    b.HasOne("InventoryManagementSystem.Domain.Entities.Product", "Product")
                        .WithMany("OrderLines")
                        .HasForeignKey("ProductId");

                    b.HasOne("InventoryManagementSystem.Domain.Entities.SubProduct", "SubProduct")
                        .WithMany("OrderLines")
                        .HasForeignKey("SubProductId");

                    b.Navigation("Ingredient");

                    b.Navigation("Order");

                    b.Navigation("Product");

                    b.Navigation("SubProduct");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.Price", b =>
                {
                    b.HasOne("InventoryManagementSystem.Domain.Entities.Ingredient", "Ingredient")
                        .WithMany("Prices")
                        .HasForeignKey("IngredientId");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.Product", b =>
                {
                    b.HasOne("InventoryManagementSystem.Domain.Entities.Document", "Document")
                        .WithOne("Product")
                        .HasForeignKey("InventoryManagementSystem.Domain.Entities.Product", "DocumentId");

                    b.HasOne("InventoryManagementSystem.Domain.Entities.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierId");

                    b.Navigation("Document");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.SubProduct", b =>
                {
                    b.HasOne("InventoryManagementSystem.Domain.Entities.Document", "Document")
                        .WithOne("SubProduct")
                        .HasForeignKey("InventoryManagementSystem.Domain.Entities.SubProduct", "DocumentId");

                    b.HasOne("InventoryManagementSystem.Domain.Entities.Supplier", "Supplier")
                        .WithMany("SubProducts")
                        .HasForeignKey("SupplierId");

                    b.Navigation("Document");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ProductProductType", b =>
                {
                    b.HasOne("InventoryManagementSystem.Domain.Entities.ProductType", null)
                        .WithMany()
                        .HasForeignKey("ProductTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryManagementSystem.Domain.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductSubProduct", b =>
                {
                    b.HasOne("InventoryManagementSystem.Domain.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryManagementSystem.Domain.Entities.SubProduct", null)
                        .WithMany()
                        .HasForeignKey("SubProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.Document", b =>
                {
                    b.Navigation("Order");

                    b.Navigation("Product");

                    b.Navigation("SubProduct");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.Ingredient", b =>
                {
                    b.Navigation("OrderLines");

                    b.Navigation("Prices");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.Product", b =>
                {
                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.SubProduct", b =>
                {
                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("InventoryManagementSystem.Domain.Entities.Supplier", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("SubProducts");
                });
#pragma warning restore 612, 618
        }
    }
}