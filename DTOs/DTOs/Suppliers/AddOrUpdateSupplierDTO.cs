using NetTopologySuite.Geometries;

namespace DTOs.DTOs.Suppliers

{
    public class AddOrUpdateSupplierDTO
    {
        public string? SupplierName { get; set; }
        public Point? Location { get; set; }
        public string? Address { get; set; }
        public List<AddOrUpdateSupplierAssetsDTO> Assets { get; set; }
    }
}
