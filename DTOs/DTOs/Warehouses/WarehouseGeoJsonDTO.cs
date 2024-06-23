using Models.Models;

namespace Models.DTOs
{
    public class WarehouseGeoJsonDTO
    {
        public string type => "Feature";
        public WarehouseGeometryDTO geometry { get; set; }
        public WarehousePropertiesDTO properties { get; set; }
        public WarehouseGeoJsonDTO(Warehouse warehouse)
        {
            geometry = new WarehouseGeometryDTO
            {
                type = "Point",
                coordinates = new double[] { warehouse.Location.X, warehouse.Location.Y }
            };
            properties = new WarehousePropertiesDTO
            {
                warehouseID = warehouse.WarehouseID,
                warehouseName = warehouse.WarehouseName,
                address = warehouse.Address,
                WarehouseAssets = warehouse.WarehouseAssets,
                WarehouseProcesses = warehouse.WarehouseProcesses,
                DeliveryProcessWSt = warehouse.DeliveryProcessWSt,
                WarehouseRequests = warehouse.WarehouseRequests,
                StoreRequests = warehouse.StoreRequests,
            };
        }
    }

    public class WarehouseGeometryDTO
    {
        public string type { get; set; }
        public double[] coordinates { get; set; }
    }

    public class WarehousePropertiesDTO
    {
        public int warehouseID { get; set; }
        public string warehouseName { get; set; }
        public string address { get; set; }
        public List<WarehouseAsset> WarehouseAssets { get; set; }
        public List<WarehouseProcess> WarehouseProcesses { get; set; }
        public List<DeliveryProcessWSt> DeliveryProcessWSt { get; set; }
        public List<WarehouseRequest> WarehouseRequests { get; set; }
        public List<StoreRequest> StoreRequests { get; set; }
    }
}
