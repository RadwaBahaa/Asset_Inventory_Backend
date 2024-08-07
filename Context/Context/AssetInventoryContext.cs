﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Context.Context
{
    public class AssetInventoryContext : IdentityDbContext<IdentityUser>
    {
        public AssetInventoryContext(DbContextOptions<AssetInventoryContext> options) : base(options) { }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetShipmentSuW> AssetShipmentSuW { get; set; }
        public DbSet<AssetShipmentWSt> AssetShipmentWSt { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DeliveryProcessSuW> DeliveryProcessSuW { get; set; }
        public DbSet<DeliveryProcessWSt> DeliveryProcessWSt { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreAsset> StoreAssets { get; set; }
        public DbSet<StoreProcess> StoreProcesses { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierAsset> SupplierAssets { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseAsset> WarehouseAssets { get; set; }
        public DbSet<WarehouseProcess> WarehouseProcesses { get; set; }
        public DbSet<WarehouseRequest> WarehouseRequests { get; set; }
        public DbSet<StoreRequest> StoreRequests { get; set; }
        public DbSet<WarehouseRequestAsset> WarehouseRequestAssets { get; set; }
        public DbSet<StoreRequestAsset> StoreRequestAssets { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Asset>(asset =>
            {
                //Primary Key
                asset.HasKey(a => a.AssetID);
                //Properties
                asset.Property(a => a.AssetName).IsRequired().HasMaxLength(50);
                asset.Property(a => a.Description).HasMaxLength(500);
                //Relations
                asset.HasOne(a => a.Category)
                    .WithMany(c => c.Assets)
                    .HasForeignKey(a => a.CategoryID)
                    .OnDelete(DeleteBehavior.Cascade);
                asset.HasMany(a => a.StoreAssets)
                    .WithOne(sa => sa.Asset)
                    .HasForeignKey(sa => sa.AssetID)
                    .OnDelete(DeleteBehavior.Cascade);
                asset.HasMany(a => a.SupplierAssets)
                    .WithOne(sa => sa.Asset)
                    .HasForeignKey(sa => sa.AssetID)
                    .OnDelete(DeleteBehavior.Cascade);
                asset.HasMany(a => a.WarehouseAssets)
                    .WithOne(wa => wa.Asset)
                    .HasForeignKey(wa => wa.AssetID)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Category>(category =>
            {
                //Primary Key
                category.HasKey(a => a.CategoryID);
                //Properties
                category.Property(c => c.CategoryName).IsRequired().HasMaxLength(50);
                category.Property(c => c.Description).HasMaxLength(500);
            });
            builder.Entity<Store>(store =>
            {
                // Primary Key
                store.HasKey(s => s.StoreID);
                // Properties
                store.Property(s=>s.StoreName).IsRequired().HasMaxLength(50);
                store.Property(s => s.Location).HasColumnType("geometry");
                // Relations
                store.HasMany(s => s.StoreAssets)
                    .WithOne(sa => sa.Store)
                    .HasForeignKey(sa => sa.StoreID)
                    .OnDelete(DeleteBehavior.Cascade);
                store.HasMany(s => s.StoreProcesses)
                    .WithOne(sp => sp.Store)
                    .HasForeignKey(sp => sp.StoreID)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Warehouse>(warehouse =>
            {
                // Primary Key
                warehouse.HasKey(w => w.WarehouseID);
                // Properties
                warehouse.Property(w=>w.WarehouseName).IsRequired().HasMaxLength(50);
                warehouse.Property(w => w.Location).HasColumnType("geometry");
                // Relations
                warehouse.HasMany(w => w.WarehouseAssets)
                    .WithOne(wa => wa.Warehouse)
                    .HasForeignKey(wa => wa.WarehouseID)
                    .OnDelete(DeleteBehavior.Cascade);
                warehouse.HasMany(w => w.WarehouseProcesses)
                    .WithOne(wp => wp.Warehouse)
                    .HasForeignKey(wp => wp.WarehouseID)
                    .OnDelete(DeleteBehavior.Cascade);
                warehouse.HasMany(w => w.DeliveryProcessWSt)
                    .WithOne(d => d.Warehouse)
                    .HasForeignKey(d => d.WarehouseID)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Supplier>(supplier =>
            {
                // Primary Key
                supplier.HasKey(s => s.SupplierID);
                // Properties
                supplier.Property(s => s.SupplierName).IsRequired().HasMaxLength(50);
                supplier.Property(s => s.Location).HasColumnType("geometry");
                // Relations
                supplier.HasMany(s => s.SupplierAssets)
                    .WithOne(sa => sa.Supplier)
                    .HasForeignKey(sa => sa.SupplierID)
                    .OnDelete(DeleteBehavior.Cascade);
                supplier.HasMany(s => s.DeliveryProcessSuW)
                    .WithOne(d => d.Supplier)
                    .HasForeignKey(d => d.SupplierID)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<DeliveryProcessSuW>(deliveryProcessSuW =>
            {
                // Primary Key
                deliveryProcessSuW.HasKey(dp => dp.ProcessID);
                // Relations
                deliveryProcessSuW.HasMany(dp => dp.WarehouseProcesses)
                    .WithOne(wp => wp.DeliveryProcessSuW)
                    .HasForeignKey(wp => wp.ProcessID)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<DeliveryProcessWSt>(deliveryProcessWSt =>
            {
                // Primary Key
                deliveryProcessWSt.HasKey(dp => dp.ProcessID);
                // Relations
                deliveryProcessWSt.HasMany(dp => dp.StoreProcesses)
                    .WithOne(sp => sp.DeliveryProcessWSt)
                    .HasForeignKey(sp => sp.ProcessID)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<SupplierAsset>(supplierAsset =>
            {
                // Primary Key
                supplierAsset.HasKey(sa => new { sa.AssetID, sa.SupplierID, sa.SerialNumber });
                //Properties
                supplierAsset.Property(sa => sa.Count).IsRequired();
            });
            builder.Entity<WarehouseAsset>(warehouseAsset =>
            {
                // Primary Key
                warehouseAsset.HasKey(wa => new { wa.AssetID, wa.WarehouseID, wa.SerialNumber });
                // Properties
                warehouseAsset.Property(wa => wa.Count).IsRequired();
            });
            builder.Entity<StoreAsset>(storeAsset =>
            {
                //Primary Key
                storeAsset.HasKey(sa => new { sa.AssetID, sa.StoreID, sa.SerialNumber });
                //Properties
                storeAsset.Property(sa=>sa.Count).IsRequired();
            });
            builder.Entity<AssetShipmentSuW>(assetShipment =>
            {
                //Primary Key
                assetShipment.HasKey(ash => new { ash.AssetID, ash.SupplierID, ash.SerialNumber, ash.ProcessID, ash.WarehouseID });
                // Relations
                assetShipment.HasOne(ash=>ash.SupplierAsset)
                    .WithMany(sa => sa.AssetShipment)
                    .HasForeignKey(ash => new { ash.AssetID, ash.WarehouseID, ash.SerialNumber })
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<AssetShipmentWSt>(assetShipment =>
            {
                //Primary Key
                assetShipment.HasKey(ash => new { ash.AssetID, ash.WarehouseID, ash.SerialNumber, ash.ProcessID, ash.StoreID });
                // Relations
                assetShipment.HasOne(ash => ash.WarehouseAsset)
                        .WithMany(sa => sa.AssetShipment)
                        .HasForeignKey(ash => new { ash.AssetID, ash.WarehouseID, ash.SerialNumber })
                        .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<WarehouseProcess>(warehouseProcess =>
            {
                //Primary Key
                warehouseProcess.HasKey(wp => new { wp.ProcessID, wp.WarehouseID });
                //Relations
                warehouseProcess.HasMany(wp => wp.AssetShipment)
                    .WithOne(ash => ash.WarehouseProcess)
                    .HasForeignKey(ash => new { ash.ProcessID, ash.WarehouseID })
                    .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<StoreProcess>(storeProcess =>
            {
                //Primary Key
                storeProcess.HasKey(sp => new { sp.ProcessID, sp.StoreID });
                //Relations
                storeProcess.HasMany(sp => sp.AssetShipment)
                    .WithOne(ash => ash.StoreProcess)
                    .HasForeignKey(ash => new { ash.ProcessID, ash.StoreID })
                    .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<WarehouseRequest>(warehouseRequest =>
            {
                //Primary Key
                warehouseRequest.HasKey(wr => wr.RequestID);
                //Relations
                warehouseRequest.HasOne(wr => wr.Supplier)
                    .WithMany(s => s.WarehouseRequests)
                    .HasForeignKey(wr => wr.SupplierID);
                warehouseRequest.HasOne(wr => wr.Warehouse)
                    .WithMany(w => w.WarehouseRequests)
                    .HasForeignKey(wr => wr.WarehouseID);
            });
            builder.Entity<StoreRequest>(storeRequest =>
            {
                //Primary Key
                storeRequest.HasKey(wr => wr.RequestID);
                //Relations
                storeRequest.HasOne(wr => wr.Warehouse)
                    .WithMany(s => s.StoreRequests)
                    .HasForeignKey(wr => wr.WarehouseID);
                storeRequest.HasOne(wr => wr.Store)
                    .WithMany(w => w.StoreRequests)
                    .HasForeignKey(wr => wr.StoreID);
            });
            builder.Entity<WarehouseRequestAsset>(warehouseRequestAseset =>
            {
                // Primary Key
                warehouseRequestAseset.HasKey(wr => new { wr.RequestID, wr.AsesetID });
                // Relations
                warehouseRequestAseset.HasOne(wra => wra.Asset)
                    .WithMany(a => a.WarehouseRequestAssets)
                    .HasForeignKey(wra => wra.AsesetID);
                warehouseRequestAseset.HasOne(wra => wra.WarehouseRequest)
                    .WithMany(a => a.WarehouseRequestAsesets)
                    .HasForeignKey(wra => wra.RequestID);
            });
            builder.Entity<StoreRequestAsset>(storeRequestAseset =>
            {
                // Primary Key
                storeRequestAseset.HasKey(wr => new { wr.RequestID, wr.AsesetID });
                // Relations
                storeRequestAseset.HasOne(wra => wra.Asset)
                    .WithMany(a => a.StoreRequestAssets)
                    .HasForeignKey(wra => wra.AsesetID);
                storeRequestAseset.HasOne(wra => wra.StoreRequest)
                    .WithMany(a => a.StoreRequestAssests)
                    .HasForeignKey(wra => wra.RequestID);
            });
            base.OnModelCreating(builder);
        }
    }
}
