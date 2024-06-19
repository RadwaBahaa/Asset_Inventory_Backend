using NetTopologySuite.Geometries;

namespace DTOs.DTOs.Stores
{
    public class AddOrUpdateStoreDTO
    {
        public string? StoreName { get; set; }
        public Point? Location { get; set; }
        public string? Address { get; set; }
        public List<AddOrUpdateStoreAssetsDTO> Assets { get; set; }
    }
}
