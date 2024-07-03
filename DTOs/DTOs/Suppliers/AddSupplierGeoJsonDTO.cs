using DTOs.DTOs;

namespace Models.DTOs
{
    public class AddSupplierGeoJsonDTO
    {
        public string type { get; set; } = "Feature";
        public GeometryDTO geometry { get; set; }
        public AddSupplierPropertiesDTO properties { get; set; }
    }

    public class AddSupplierPropertiesDTO
    {
        public string supplierName { get; set; }
        public string? address { get; set; }
    }
}