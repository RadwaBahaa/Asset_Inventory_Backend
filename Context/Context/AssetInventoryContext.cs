using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Asset>(asset =>
            {
                //Relations
                asset.HasOne(a => a.Category)
                    .WithMany(c => c.Assets)
                    .HasForeignKey(a => a.CategoryID);
                asset.HasMany(a => a.SupplierAssets)
                    .WithOne(sa => sa.Asset)
                    .HasForeignKey(sa => sa.AssetID);
                asset.HasMany(a => a.WarehouseAssets)
                    .WithOne(wa => wa.Asset)
                    .HasForeignKey(wa => wa.AssetID);
                asset.HasMany(a => a.StoreAssets)
                    .WithOne(sa => sa.Asset)
                    .HasForeignKey(sa => sa.AssetID);
            });
            builder.Entity<Store>(store =>
            {
                //Relations
                store.HasMany(s => s.StoreAssets)
                    .WithOne(sa => sa.Store)
                    .HasForeignKey(sa => sa.StoreID);
                store.HasMany(s => s.StoreProcesses)
                    .WithOne(sp => sp.Store)
                    .HasForeignKey(sp => sp.StoreID);
                //Properties
                store.Property(s => s.Location).HasColumnType("geometry");
            });
            builder.Entity<Warehouse>(warehouse =>
            {
                //Relations
                warehouse.HasMany(w => w.WarehouseAssets)
                    .WithOne(wa => wa.Warehouse)
                    .HasForeignKey(wa => wa.WarehouseID);
                warehouse.HasMany(w => w.WarehouseProcesses)
                    .WithOne(wp => wp.Warehouse)
                    .HasForeignKey(wp => wp.WarehouseID);
                warehouse.HasMany(w => w.DeliveryProcessWSt)
                    .WithOne(d => d.Warehouse)
                    .HasForeignKey(d => d.WarehouseID);
                //Properties
                warehouse.Property(w => w.Location).HasColumnType("geometry");
            });
            builder.Entity<Supplier>(supplier =>
            {
                supplier.HasMany(s => s.SupplierAssets)
                    .WithOne(sa => sa.Supplier)
                    .HasForeignKey(sa => sa.SupplierID);
                supplier.HasMany(s => s.DeliveryProcessSuW)
                    .WithOne(d => d.Supplier)
                    .HasForeignKey(d => d.SupplierID);
                //Properties
                supplier.Property(s => s.Location).HasColumnType("geometry");
            });
            builder.Entity<DeliveryProcessSuW>(deliveryProcessSuW =>
            {
                //Relations
                deliveryProcessSuW.HasMany(dp => dp.WarehouseProcesses)
                    .WithOne(wp => wp.DeliveryProcessSuW)
                    .HasForeignKey(wp => wp.ProcessID);
                deliveryProcessSuW.HasMany(dp => dp.AssetShipmentSuW)
                    .WithOne(ash => ash.DeliveryProcessSuW)
                    .HasForeignKey(ash => ash.ProcessID);
            });
            builder.Entity<DeliveryProcessWSt>(deliveryProcessWSt =>
            {
                //Relations
                deliveryProcessWSt.HasMany(dp => dp.StoreProcesses)
                    .WithOne(sp => sp.DeliveryProcessWSt)
                    .HasForeignKey(sp => sp.ProcessID);
                deliveryProcessWSt.HasMany(dp => dp.AssetShipmentWSt)
                    .WithOne(ash => ash.DeliveryProcessWSt)
                    .HasForeignKey(ash => ash.ProcessID);
            });

            builder.Entity<SupplierAsset>(supplierAsset =>
            {
                //Primary Key
                supplierAsset.HasKey(sa => sa.AssetID);
                supplierAsset.HasKey(sa => sa.SupplierID);
                //Relations
                supplierAsset.HasMany(sa => sa.AssetShipmentSuW)
                    .WithOne(ash => ash.SupplierAsset)
                    .HasForeignKey(ash => ash.AssetID)
                    .HasForeignKey(ash => ash.SupplierID);
            });
            builder.Entity<WarehouseAsset>(warehouseAsset =>
            {
                //Primary Key
                warehouseAsset.HasKey(wa => wa.AssetID);
                warehouseAsset.HasKey(wa => wa.WarehouseID);
                //Relations
                warehouseAsset.HasMany(wa => wa.AssetShipmentWSts)
                    .WithOne(ash => ash.WarehouseAsset)
                    .HasForeignKey(ash => ash.AssetID)
                    .HasForeignKey(ash => ash.WarehouseAsset);
            });
            builder.Entity<StoreAsset>(storeAsset =>
            {
                //Primary Key
                storeAsset.HasKey(sa => sa.AssetID);
                storeAsset.HasKey(sa => sa.StoreID);
            });

            builder.Entity<AssetShipmentSuW>(assetShipment =>
            {
                //Primary Key
                assetShipment.HasKey(ash => ash.AssetID);
                assetShipment.HasKey(ash => ash.SupplierID);
                assetShipment.HasKey(ash => ash.ProcessID);
            });
            builder.Entity<AssetShipmentWSt>(assetShipment =>
            {
                //Primary Key
                assetShipment.HasKey(ash => ash.AssetID);
                assetShipment.HasKey(ash => ash.WarehouseID);
                assetShipment.HasKey(ash => ash.ProcessID);
            });

            builder.Entity<WarehouseProcess>(warehouseProcess =>
            {
                //Primary key
                warehouseProcess.HasKey(wp => wp.ProcessID);
                warehouseProcess.HasKey(wp => wp.WarehouseID);
            });
            builder.Entity<StoreProcess>(storeProcess =>
            {
                //Primary key
                storeProcess.HasKey(sp => sp.ProcessID);
                storeProcess.HasKey(sp => sp.StoreID);
            });

            base.OnModelCreating(builder);
        }
    }
}
