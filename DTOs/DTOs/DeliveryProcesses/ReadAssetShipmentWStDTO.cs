using DTOs.DTOs.Warehouses;

namespace DTOs.DTOs.DeliveryProcesses
{
    public class ReadAssetShipmentWStDTO
    {
        public ReadWarehouseAssetsDTO WarehouseAsset { get; set; }
        public int Quantity { get; set; }
    }
}
