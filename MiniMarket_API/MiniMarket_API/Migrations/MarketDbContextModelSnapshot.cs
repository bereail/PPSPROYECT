﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniMarket_API.Model;

#nullable disable

namespace MiniMarket_Server_dev.Migrations
{
    [DbContext(typeof(MarketDbContext))]
    partial class MarketDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MiniMarket_API.Model.Entities.CompanyCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("EmployeeCodes");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.DeliveryAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Apartment")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(45)");

                    b.Property<int?>("Floor")
                        .HasColumnType("int");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("DeliveryAddresses");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.OrderDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("DetailPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Item");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("Details");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeactivationTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.ProductCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("DeactivationTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.ProductImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Extension");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(35)")
                        .HasColumnName("Name");

                    b.Property<long>("ImageSize")
                        .HasColumnType("bigint")
                        .HasColumnName("Size");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("URL");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.SaleOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ExpirationTime")
                        .HasColumnType("datetime");

                    b.Property<decimal>("FinalPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("FinishTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeactivationTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(75)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("Role");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("UserType").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.Customer", b =>
                {
                    b.HasBaseType("MiniMarket_API.Model.Entities.User");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.Seller", b =>
                {
                    b.HasBaseType("MiniMarket_API.Model.Entities.User");

                    b.Property<Guid>("CompanyCodeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("CompanyCodeId")
                        .IsUnique()
                        .HasFilter("[CompanyCodeId] IS NOT NULL");

                    b.HasDiscriminator().HasValue("Seller");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.SuperAdmin", b =>
                {
                    b.HasBaseType("MiniMarket_API.Model.Entities.User");

                    b.HasDiscriminator().HasValue("SuperAdmin");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.DeliveryAddress", b =>
                {
                    b.HasOne("MiniMarket_API.Model.Entities.User", "User")
                        .WithOne("DeliveryAddress")
                        .HasForeignKey("MiniMarket_API.Model.Entities.DeliveryAddress", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.OrderDetails", b =>
                {
                    b.HasOne("MiniMarket_API.Model.Entities.SaleOrder", "SaleOrder")
                        .WithMany("Details")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniMarket_API.Model.Entities.Product", "Product")
                        .WithMany("Details")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("SaleOrder");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.Product", b =>
                {
                    b.HasOne("MiniMarket_API.Model.Entities.ProductCategory", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.ProductImage", b =>
                {
                    b.HasOne("MiniMarket_API.Model.Entities.Product", "Product")
                        .WithOne("ProductImage")
                        .HasForeignKey("MiniMarket_API.Model.Entities.ProductImage", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.SaleOrder", b =>
                {
                    b.HasOne("MiniMarket_API.Model.Entities.DeliveryAddress", "DeliveryAddress")
                        .WithMany("SaleOrders")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MiniMarket_API.Model.Entities.User", "User")
                        .WithMany("SaleOrders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliveryAddress");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.Seller", b =>
                {
                    b.HasOne("MiniMarket_API.Model.Entities.CompanyCode", "CompanyCode")
                        .WithOne("Seller")
                        .HasForeignKey("MiniMarket_API.Model.Entities.Seller", "CompanyCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompanyCode");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.CompanyCode", b =>
                {
                    b.Navigation("Seller");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.DeliveryAddress", b =>
                {
                    b.Navigation("SaleOrders");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.Product", b =>
                {
                    b.Navigation("Details");

                    b.Navigation("ProductImage");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.SaleOrder", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("MiniMarket_API.Model.Entities.User", b =>
                {
                    b.Navigation("DeliveryAddress");

                    b.Navigation("SaleOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
