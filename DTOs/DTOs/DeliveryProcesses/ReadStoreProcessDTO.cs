using DTOs.DTOs.Stores;
using DTOs.DTOs.Warehouses;

namespace DTOs.DTOs.DeliveryProcesses
{
    public class ReadStoreProcessDTO
    {
        public int StoreID { get; set; }  
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public ReadStoreDTO Store { get; set; }
        public List<ReadAssetShipmentWStDTO> AssetShipment { get; set; }
    }
}
