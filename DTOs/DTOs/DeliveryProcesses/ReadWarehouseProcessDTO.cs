using DTOs.DTOs.Suppliers;
using DTOs.DTOs.Warehouses;

namespace DTOs.DTOs.DeliveryProcesses
{
    public class ReadWarehouseProcessDTO
    {
        public int WarehouseID { get; set; }    
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public ReadWarehouseDTO Warehouse { get; set; }
        public List<ReadAssetShipmentSuWDTO> AssetShipment { get; set; }
    }
}
