using DTOs.DTOs.Suppliers;

namespace DTOs.DTOs.DeliveryProcesses
{
    public class ReadAssetShipmentSuWDTO
    {       
        public ReadSupplierAssetsDTO SupplierAsset { get; set; }
        public int Quantity { get; set; }
    }
}
