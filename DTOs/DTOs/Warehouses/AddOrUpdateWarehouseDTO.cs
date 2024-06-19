using NetTopologySuite.Geometries;

namespace DTOs.DTOs.Warehouses

{
    public class AddOrUpdateWarehouseDTO
    {
        public string? WarehouseName { get; set; }
        public Point? Location { get; set; }
        public string? Address { get; set; }
        public List<AddOrUpdateWarehouseAssetsDTO> Assets { get; set; }
    }
}
