using DTOs.DTOs;
using Models.Models;

namespace Models.DTOs
{
    public class ReadWarehouseGeoJsonDTO
    {
        public string type => "Feature";
        public GeometryDTO geometry { get; set; }
        public WarehousePropertiesDTO properties { get; set; }
        public ReadWarehouseGeoJsonDTO(Warehouse warehouse)
        {
            geometry = new GeometryDTO
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
