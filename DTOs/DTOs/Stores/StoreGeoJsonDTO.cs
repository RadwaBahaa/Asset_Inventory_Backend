using Models.Models;

namespace Models.DTOs
{
    public class StoreGeoJsonDTO
    {
        public string type => "Feature";
        public StoreGeometryDTO geometry { get; set; }
        public StorePropertiesDTO properties { get; set; }
        public StoreGeoJsonDTO(Store store)
        {
            geometry = new StoreGeometryDTO
            {
                type = "Point",
                coordinates = new double[] { store.Location.X, store.Location.Y }
            };
            properties = new StorePropertiesDTO
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

    public class StoreGeometryDTO
    {
        public string type { get; set; }
        public double[] coordinates { get; set; }
    }

    public class StorePropertiesDTO
    {
        public int storeID { get; set; }
        public string storeName { get; set; }
        public string address { get; set; }
        public List<StoreAsset> StoreAssets { get; set; }
        public List<StoreProcess> StoreProcesses { get; set; }
        public List<StoreRequest> StoreRequests { get; set; }
    }
}
