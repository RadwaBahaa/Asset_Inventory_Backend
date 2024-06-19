using NetTopologySuite.Geometries;

namespace DTOs.DTOs.Warehouses
{
    public class ReadWarehouseDTO
    {
        public int WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public Point Location { get; set; }
        public string Address { get; set; }
        public List<ReadWarehouseAssetsDTO> Assets { get; set; }
    }
}
