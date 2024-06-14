using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Context.Context
{
    public class AssetInventoryContext : DbContext
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
                asset.HasKey(a => a.AssetID);
                asset.HasOne(a => a.Category)
                    .WithMany(c => c.Assets)
                    .HasForeignKey(a => a.CategoryID)
                    .OnDelete(DeleteBehavior.Cascade);
                asset.HasMany(a => a.SupplierAssets)
                    .WithOne(sa => sa.Asset)
                    .HasForeignKey(sa => sa.AssetID)
                    .OnDelete(DeleteBehavior.Cascade);
                asset.HasMany(a => a.WarehouseAssets)
                    .WithOne(wa => wa.Asset)
                    .HasForeignKey(wa => wa.AssetID)
                    .OnDelete(DeleteBehavior.Cascade);
                asset.HasMany(a => a.StoreAssets)
                    .WithOne(sa => sa.Asset)
                    .HasForeignKey(sa => sa.AssetID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Store>(store =>
            {
                store.HasKey(s => s.StoreID);
                store.HasMany(s => s.StoreAssets)
                    .WithOne(sa => sa.Store)
                    .HasForeignKey(sa => sa.StoreID)
                    .OnDelete(DeleteBehavior.Cascade);
                store.HasMany(s => s.StoreProcesses)
                    .WithOne(sp => sp.Store)
                    .HasForeignKey(sp => sp.StoreID)
                    .OnDelete(DeleteBehavior.Cascade);
                store.Property(s => s.Location).HasColumnType("geometry");
            });

            builder.Entity<Warehouse>(warehouse =>
            {
                warehouse.HasKey(w => w.WarehouseID);
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
                warehouse.Property(w => w.Location).HasColumnType("geometry");
            });

            builder.Entity<Supplier>(supplier =>
            {
                supplier.HasKey(s => s.SupplierID);
                supplier.HasMany(s => s.SupplierAssets)
                    .WithOne(sa => sa.Supplier)
                    .HasForeignKey(sa => sa.SupplierID)
                    .OnDelete(DeleteBehavior.Cascade);
                supplier.HasMany(s => s.DeliveryProcessSuW)
                    .WithOne(d => d.Supplier)
                    .HasForeignKey(d => d.SupplierID)
                    .OnDelete(DeleteBehavior.Cascade);
                supplier.Property(s => s.Location).HasColumnType("geometry");
            });

            builder.Entity<DeliveryProcessSuW>(deliveryProcessSuW =>
            {
                deliveryProcessSuW.HasKey(dp => dp.ProcessID);
                deliveryProcessSuW.HasMany(dp => dp.WarehouseProcesses)
                    .WithOne(wp => wp.DeliveryProcessSuW)
                    .HasForeignKey(wp => wp.ProcessID)
                    .OnDelete(DeleteBehavior.Cascade);
                deliveryProcessSuW.HasMany(dp => dp.AssetShipmentSuW)
                    .WithOne(ash => ash.DeliveryProcessSuW)
                    .HasForeignKey(ash => ash.ProcessID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<DeliveryProcessWSt>(deliveryProcessWSt =>
            {
                deliveryProcessWSt.HasKey(dp => dp.ProcessID);
                deliveryProcessWSt.HasMany(dp => dp.StoreProcesses)
                    .WithOne(sp => sp.DeliveryProcessWSt)
                    .HasForeignKey(sp => sp.ProcessID)
                    .OnDelete(DeleteBehavior.Cascade);
                deliveryProcessWSt.HasMany(dp => dp.AssetShipmentWSt)
                    .WithOne(ash => ash.DeliveryProcessWSt)
                    .HasForeignKey(ash => ash.ProcessID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<SupplierAsset>(supplierAsset =>
            {
                supplierAsset.HasKey(sa => new { sa.AssetID, sa.SupplierID });
                supplierAsset.HasMany(sa => sa.AssetShipmentSuW)
                    .WithOne(ash => ash.SupplierAsset)
                    .HasForeignKey(ash => new { ash.AssetID, ash.SupplierID })
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<WarehouseAsset>(warehouseAsset =>
            {
                warehouseAsset.HasKey(wa => new { wa.AssetID, wa.WarehouseID });
                warehouseAsset.HasMany(wa => wa.AssetShipmentWSts)
                    .WithOne(ash => ash.WarehouseAsset)
                    .HasForeignKey(ash => new { ash.AssetID, ash.WarehouseID })
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<StoreAsset>(storeAsset =>
            {
                storeAsset.HasKey(sa => new { sa.AssetID, sa.StoreID });
            });

            builder.Entity<AssetShipmentSuW>(assetShipment =>
            {
                assetShipment.HasKey(ash => new { ash.AssetID, ash.SupplierID, ash.ProcessID });
            });

            builder.Entity<AssetShipmentWSt>(assetShipment =>
            {
                assetShipment.HasKey(ash => new { ash.AssetID, ash.WarehouseID, ash.ProcessID });
            });

            builder.Entity<WarehouseProcess>(warehouseProcess =>
            {
                warehouseProcess.HasKey(wp => new { wp.ProcessID, wp.WarehouseID });
            });

            builder.Entity<StoreProcess>(storeProcess =>
            {
                storeProcess.HasKey(sp => new { sp.ProcessID, sp.StoreID });
            });

            base.OnModelCreating(builder);
        }
    }
}
