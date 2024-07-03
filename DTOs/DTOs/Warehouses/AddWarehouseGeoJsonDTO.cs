using DTOs.DTOs;

namespace Models.DTOs
{
    public class AddWarehouseGeoJsonDTO
    {
        public string type { get; set; } = "Feature";
        public GeometryDTO geometry { get; set; }
        public AddWarehousePropertiesDTO properties { get; set; }
    }

    public class AddWarehousePropertiesDTO
    {
        public string warehouseName { get; set; }
        public string? address { get; set; }
    }
}