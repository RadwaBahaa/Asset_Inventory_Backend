using DTOs.DTOs.Categories;
using DTOs.DTOs.Stores;
using System.Drawing;

namespace DTOs.DTOs.Warehouses
{
    public class ReadWarehouseDTO
    {
        public string WarehouseName { get; set; }
        public Point Location { get; set; }
        public string Address { get; set; }
        public ReadWarehouseAssetsDTO WarehouseAssets { get; set; }
    }
}
