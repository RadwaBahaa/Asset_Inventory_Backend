using DTOs.DTOs.Assets;

namespace DTOs.DTOs.Stores
{
    public class ReadStoreAssetsDTO
    {
        public int AssetID { get; set; }
        public string SerialNumber { get; set; }
        public int Count { get; set; }
        public ReadAssetDTO Asset { get; set; }
    }
}
