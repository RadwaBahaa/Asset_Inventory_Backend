﻿using DTOs.DTOs;
using Models.Models;

namespace Models.DTOs
{
    public class ReadSupplierGeoJsonDTO
    {
        public string type => "Feature";
        public GeometryDTO geometry { get; set; }
        public SupplierPropertiesDTO properties { get; set; }
        public ReadSupplierGeoJsonDTO(Supplier supplier)
        {
            geometry = new GeometryDTO
            {
                type = "Point",
                coordinates = new double[] { supplier.Location.X, supplier.Location.Y }
            };
            properties = new SupplierPropertiesDTO
            {
                supplierID = supplier.SupplierID,
                supplierName = supplier.SupplierName,
                address = supplier.Address,
            };
        }
    }

    public class SupplierPropertiesDTO
    {
        public int supplierID { get; set; }
        public string supplierName { get; set; }
        public string address { get; set; }
    }
}
