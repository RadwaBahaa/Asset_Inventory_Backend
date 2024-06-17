using NetTopologySuite.Geometries;

namespace Models.Models
{
    public class Warehouse
    {
        public int WarehouseID { get; set; }    // Primary Key
        public string WarehouseName { get; set; }
        public Point Location { get; set; }
        public string Address { get; set; }


        //RELATIONS
        public List<WarehouseAsset> WarehouseAssets { get; set; }
        public List<WarehouseProcess> WarehouseProcesses {  get; set; }
        public List<DeliveryProcessWSt> DeliveryProcessWSt { get; set; }
        public List<WarehouseRequest> WarehouseRequests { get; set; }
        public List<StoreRequest> StoreRequests { get; set; }  
    }
}
