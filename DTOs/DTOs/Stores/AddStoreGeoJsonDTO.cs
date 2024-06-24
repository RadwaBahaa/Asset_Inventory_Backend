using DTOs.DTOs;

namespace Models.DTOs
{
    public class AddStoreGeoJsonDTO
    {
        public string type { get; set; } = "Feature";
        public GeometryDTO geometry { get; set; }
        public AddStorePropertiesDTO properties { get; set; }
    }

    public class AddStorePropertiesDTO
    {
        public string storeName { get; set; }
        public string address { get; set; }
    }
}