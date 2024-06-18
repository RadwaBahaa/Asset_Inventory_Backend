using DTOs.DTOs.Stores;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.Warehouses

{
    public class AddOrUpdateWarehouseDTO
    {
        public string? WarehouseName { get; set; }
        public Point? Location { get; set; }
        public string? Address { get; set; }
        public AddOrUpdateWarehouseAssetsDTO WarehouseAssets { get; set; }
    }
}
