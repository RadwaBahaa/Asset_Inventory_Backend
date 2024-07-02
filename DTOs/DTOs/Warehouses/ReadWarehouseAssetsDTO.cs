using DTOs.DTOs.Assets;

namespace DTOs.DTOs.Warehouses
{
    public class ReadWarehouseAssetsDTO
    {
        public int AssetID { get; set; }
        public string SerialNumber { get; set; }
        public int Count { get; set; }
        public ReadAssetDTO Asset { get; set; }
    }
}
