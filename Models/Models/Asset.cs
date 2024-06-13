﻿namespace Models.Models
{
    public class Asset
    {
        public int AssetID { get; set; }
        public string AssetName { get; set; }
        public int SerialNumber { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public int Price { get; set; }
        public DateOnly Creation_Date { get; set; }


        //RELATIONS
        public Category Categories { get; set; }
        public List<SupplierAsset> SupplierAssets { get; set; }
        public List<WarehouseAsset> WarehouseAssets { get; set; }
        public List<StoreAsset> StoreAssets { get; set; }
    }
}
