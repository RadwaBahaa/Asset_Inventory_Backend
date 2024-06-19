﻿namespace DTOs.DTOs.Warehouses

{
    public class AddOrUpdateWarehouseAssetsDTO
    {
        public int? AssetID { get; set; }
        public int? SerialNumber { get; set; }
        public DateOnly? ProductionDate { get; set; }
        public int? Count { get; set; }
    }
}
