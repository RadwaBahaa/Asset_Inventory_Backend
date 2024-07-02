using DTOs.DTOs;
using Models.Models;

namespace Models.DTOs
{
    public class ReadStoreGeoJsonDTO
    {
        public string type => "Feature";
        public GeometryDTO geometry { get; set; }
        public ReadStorePropertiesDTO properties { get; set; }
        public ReadStoreGeoJsonDTO(Store store)
        {
            geometry = new GeometryDTO
            {
                type = "Point",
                coordinates = new double[] { store.Location.X, store.Location.Y }
            };
            properties = new ReadStorePropertiesDTO
            {
                storeID = store.StoreID,
                storeName = store.StoreName,
                address = store.Address,
            };
        }
    }

    public class ReadStorePropertiesDTO
    {
        public int storeID { get; set; }
        public string storeName { get; set; }
        public string address { get; set; }
    }
}
