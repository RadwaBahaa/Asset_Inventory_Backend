﻿// <auto-generated />
using System;
using Context.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

#nullable disable

namespace Context.Migrations
{
    [DbContext(typeof(AssetInventoryContext))]
    partial class AssetInventoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Models.Asset", b =>
                {
                    b.Property<int>("AssetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssetID"));

                    b.Property<string>("AssetName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<byte[]>("Picture")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("AssetID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("Models.Models.AssetShipmentSuW", b =>
                {
                    b.Property<int>("AssetID")
                        .HasColumnType("int");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.Property<string>("SerialNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProcessID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("AssetID", "SupplierID", "SerialNo", "ProcessID");

                    b.HasIndex("ProcessID");

                    b.ToTable("AssetShipmentSuW");
                });

            modelBuilder.Entity("Models.Models.AssetShipmentWSt", b =>
                {
                    b.Property<int>("AssetID")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseID")
                        .HasColumnType("int");

                    b.Property<string>("SerialNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProcessID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("AssetID", "WarehouseID", "SerialNo", "ProcessID");

                    b.HasIndex("ProcessID");

                    b.ToTable("AssetShipmentWSt");
                });

            modelBuilder.Entity("Models.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Models.Models.DeliveryProcessSuW", b =>
                {
                    b.Property<int>("ProcessID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProcessID"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.Property<int>("TotalAssets")
                        .HasColumnType("int");

                    b.HasKey("ProcessID");

                    b.HasIndex("SupplierID");

                    b.ToTable("DeliveryProcessSuW");
                });

            modelBuilder.Entity("Models.Models.DeliveryProcessWSt", b =>
                {
                    b.Property<int>("ProcessID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProcessID"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalAssets")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseID")
                        .HasColumnType("int");

                    b.HasKey("ProcessID");

                    b.HasIndex("WarehouseID");

                    b.ToTable("DeliveryProcessWSt");
                });

            modelBuilder.Entity("Models.Models.Store", b =>
                {
                    b.Property<int>("StoreID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Point>("Location")
                        .IsRequired()
                        .HasColumnType("geometry");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StoreID");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Models.Models.StoreAsset", b =>
                {
                    b.Property<int>("AssetID")
                        .HasColumnType("int");

                    b.Property<int>("StoreID")
                        .HasColumnType("int");

                    b.Property<string>("SerialNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateOnly>("ProductionDate")
                        .HasColumnType("date");

                    b.HasKey("AssetID", "StoreID", "SerialNo");

                    b.HasIndex("StoreID");

                    b.ToTable("StoreAssets");
                });

            modelBuilder.Entity("Models.Models.StoreProcess", b =>
                {
                    b.Property<int>("ProcessID")
                        .HasColumnType("int");

                    b.Property<int>("StoreID")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProcessID", "StoreID");

                    b.HasIndex("StoreID");

                    b.ToTable("StoreProcesses");
                });

            modelBuilder.Entity("Models.Models.StoreRequest", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestID"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoreID")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseID")
                        .HasColumnType("int");

                    b.HasKey("RequestID");

                    b.HasIndex("StoreID");

                    b.HasIndex("WarehouseID");

                    b.ToTable("StoreRequests");
                });

            modelBuilder.Entity("Models.Models.StoreRequestAsset", b =>
                {
                    b.Property<int>("RequestID")
                        .HasColumnType("int");

                    b.Property<int>("AsesetID")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("RequestID", "AsesetID");

                    b.HasIndex("AsesetID");

                    b.ToTable("StoreRequestAssets");
                });

            modelBuilder.Entity("Models.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Point>("Location")
                        .IsRequired()
                        .HasColumnType("geometry");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupplierID");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Models.Models.SupplierAsset", b =>
                {
                    b.Property<int>("AssetID")
                        .HasColumnType("int");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.Property<string>("SerialNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateOnly>("ProductionDate")
                        .HasColumnType("date");

                    b.HasKey("AssetID", "SupplierID", "SerialNo");

                    b.HasIndex("SupplierID");

                    b.ToTable("SupplierAssets");
                });

            modelBuilder.Entity("Models.Models.Warehouse", b =>
                {
                    b.Property<int>("WarehouseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarehouseID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Point>("Location")
                        .IsRequired()
                        .HasColumnType("geometry");

                    b.Property<string>("WarehouseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WarehouseID");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("Models.Models.WarehouseAsset", b =>
                {
                    b.Property<int>("AssetID")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseID")
                        .HasColumnType("int");

                    b.Property<string>("SerialNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateOnly>("ProductionDate")
                        .HasColumnType("date");

                    b.HasKey("AssetID", "WarehouseID", "SerialNo");

                    b.HasIndex("WarehouseID");

                    b.ToTable("WarehouseAssets");
                });

            modelBuilder.Entity("Models.Models.WarehouseProcess", b =>
                {
                    b.Property<int>("ProcessID")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseID")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProcessID", "WarehouseID");

                    b.HasIndex("WarehouseID");

                    b.ToTable("WarehouseProcesses");
                });

            modelBuilder.Entity("Models.Models.WarehouseRequest", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestID"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseID")
                        .HasColumnType("int");

                    b.HasKey("RequestID");

                    b.HasIndex("SupplierID");

                    b.HasIndex("WarehouseID");

                    b.ToTable("WarehouseRequests");
                });

            modelBuilder.Entity("Models.Models.WarehouseRequestAsset", b =>
                {
                    b.Property<int>("RequestID")
                        .HasColumnType("int");

                    b.Property<int>("AsesetID")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("RequestID", "AsesetID");

                    b.HasIndex("AsesetID");

                    b.ToTable("WarehouseRequestAssets");
                });

            modelBuilder.Entity("Models.Models.Asset", b =>
                {
                    b.HasOne("Models.Models.Category", "Category")
                        .WithMany("Assets")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Models.Models.AssetShipmentSuW", b =>
                {
                    b.HasOne("Models.Models.DeliveryProcessSuW", "DeliveryProcessSuW")
                        .WithMany("AssetShipmentSuW")
                        .HasForeignKey("ProcessID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.Models.SupplierAsset", "SupplierAsset")
                        .WithMany("AssetShipmentSuW")
                        .HasForeignKey("AssetID", "SupplierID", "SerialNo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DeliveryProcessSuW");

                    b.Navigation("SupplierAsset");
                });

            modelBuilder.Entity("Models.Models.AssetShipmentWSt", b =>
                {
                    b.HasOne("Models.Models.DeliveryProcessWSt", "DeliveryProcessWSt")
                        .WithMany("AssetShipmentWSt")
                        .HasForeignKey("ProcessID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.Models.WarehouseAsset", "WarehouseAsset")
                        .WithMany("AssetShipmentWSts")
                        .HasForeignKey("AssetID", "WarehouseID", "SerialNo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DeliveryProcessWSt");

                    b.Navigation("WarehouseAsset");
                });

            modelBuilder.Entity("Models.Models.DeliveryProcessSuW", b =>
                {
                    b.HasOne("Models.Models.Supplier", "Supplier")
                        .WithMany("DeliveryProcessSuW")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Models.Models.DeliveryProcessWSt", b =>
                {
                    b.HasOne("Models.Models.Warehouse", "Warehouse")
                        .WithMany("DeliveryProcessWSt")
                        .HasForeignKey("WarehouseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Models.Models.StoreAsset", b =>
                {
                    b.HasOne("Models.Models.Asset", "Asset")
                        .WithMany("StoreAssets")
                        .HasForeignKey("AssetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Models.Store", "Store")
                        .WithMany("StoreAssets")
                        .HasForeignKey("StoreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("Models.Models.StoreProcess", b =>
                {
                    b.HasOne("Models.Models.DeliveryProcessWSt", "DeliveryProcessWSt")
                        .WithMany("StoreProcesses")
                        .HasForeignKey("ProcessID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Models.Store", "Store")
                        .WithMany("StoreProcesses")
                        .HasForeignKey("StoreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliveryProcessWSt");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("Models.Models.StoreRequest", b =>
                {
                    b.HasOne("Models.Models.Store", "Store")
                        .WithMany("StoreRequests")
                        .HasForeignKey("StoreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Models.Warehouse", "Warehouse")
                        .WithMany("StoreRequests")
                        .HasForeignKey("WarehouseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Models.Models.StoreRequestAsset", b =>
                {
                    b.HasOne("Models.Models.Asset", "Asset")
                        .WithMany("StoreRequestAssets")
                        .HasForeignKey("AsesetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Models.StoreRequest", "StoreRequest")
                        .WithMany("StoreRequestAssests")
                        .HasForeignKey("RequestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("StoreRequest");
                });

            modelBuilder.Entity("Models.Models.SupplierAsset", b =>
                {
                    b.HasOne("Models.Models.Asset", "Asset")
                        .WithMany("SupplierAssets")
                        .HasForeignKey("AssetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Models.Supplier", "Supplier")
                        .WithMany("SupplierAssets")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Models.Models.WarehouseAsset", b =>
                {
                    b.HasOne("Models.Models.Asset", "Asset")
                        .WithMany("WarehouseAssets")
                        .HasForeignKey("AssetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Models.Warehouse", "Warehouse")
                        .WithMany("WarehouseAssets")
                        .HasForeignKey("WarehouseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Models.Models.WarehouseProcess", b =>
                {
                    b.HasOne("Models.Models.DeliveryProcessSuW", "DeliveryProcessSuW")
                        .WithMany("WarehouseProcesses")
                        .HasForeignKey("ProcessID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Models.Warehouse", "Warehouse")
                        .WithMany("WarehouseProcesses")
                        .HasForeignKey("WarehouseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliveryProcessSuW");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Models.Models.WarehouseRequest", b =>
                {
                    b.HasOne("Models.Models.Supplier", "Supplier")
                        .WithMany("WarehouseRequests")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Models.Warehouse", "Warehouse")
                        .WithMany("WarehouseRequests")
                        .HasForeignKey("WarehouseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Models.Models.WarehouseRequestAsset", b =>
                {
                    b.HasOne("Models.Models.Asset", "Asset")
                        .WithMany("WarehouseRequestAssets")
                        .HasForeignKey("AsesetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Models.WarehouseRequest", "WarehouseRequest")
                        .WithMany("WarehouseRequestAsesets")
                        .HasForeignKey("RequestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("WarehouseRequest");
                });

            modelBuilder.Entity("Models.Models.Asset", b =>
                {
                    b.Navigation("StoreAssets");

                    b.Navigation("StoreRequestAssets");

                    b.Navigation("SupplierAssets");

                    b.Navigation("WarehouseAssets");

                    b.Navigation("WarehouseRequestAssets");
                });

            modelBuilder.Entity("Models.Models.Category", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("Models.Models.DeliveryProcessSuW", b =>
                {
                    b.Navigation("AssetShipmentSuW");

                    b.Navigation("WarehouseProcesses");
                });

            modelBuilder.Entity("Models.Models.DeliveryProcessWSt", b =>
                {
                    b.Navigation("AssetShipmentWSt");

                    b.Navigation("StoreProcesses");
                });

            modelBuilder.Entity("Models.Models.Store", b =>
                {
                    b.Navigation("StoreAssets");

                    b.Navigation("StoreProcesses");

                    b.Navigation("StoreRequests");
                });

            modelBuilder.Entity("Models.Models.StoreRequest", b =>
                {
                    b.Navigation("StoreRequestAssests");
                });

            modelBuilder.Entity("Models.Models.Supplier", b =>
                {
                    b.Navigation("DeliveryProcessSuW");

                    b.Navigation("SupplierAssets");

                    b.Navigation("WarehouseRequests");
                });

            modelBuilder.Entity("Models.Models.SupplierAsset", b =>
                {
                    b.Navigation("AssetShipmentSuW");
                });

            modelBuilder.Entity("Models.Models.Warehouse", b =>
                {
                    b.Navigation("DeliveryProcessWSt");

                    b.Navigation("StoreRequests");

                    b.Navigation("WarehouseAssets");

                    b.Navigation("WarehouseProcesses");

                    b.Navigation("WarehouseRequests");
                });

            modelBuilder.Entity("Models.Models.WarehouseAsset", b =>
                {
                    b.Navigation("AssetShipmentWSts");
                });

            modelBuilder.Entity("Models.Models.WarehouseRequest", b =>
                {
                    b.Navigation("WarehouseRequestAsesets");
                });
#pragma warning restore 612, 618
        }
    }
}
