﻿namespace DTOs.DTOs.Warehouses
{
    public class UpdateWarehouseDTO
    {
        public string? WarehouseName { get; set; }
        [LongitudeValidation]
        public double? Longitude { get; set; }
        [LatitudeValidation]
        public double? Latitude { get; set; }
        public string? Address { get; set; }
    }
}
