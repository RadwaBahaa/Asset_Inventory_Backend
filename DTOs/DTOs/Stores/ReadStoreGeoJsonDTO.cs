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
                StoreAssets = store.StoreAssets,
                StoreProcesses = store.StoreProcesses,
                StoreRequests = store.StoreRequests
            };
        }
    }

    public class ReadStorePropertiesDTO
    {
        public int storeID { get; set; }
        public string storeName { get; set; }
        public string address { get; set; }
        public List<StoreAsset> StoreAssets { get; set; }
        public List<StoreProcess> StoreProcesses { get; set; }
        public List<StoreRequest> StoreRequests { get; set; }
    }
}
