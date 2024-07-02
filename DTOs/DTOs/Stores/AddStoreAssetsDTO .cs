namespace DTOs.DTOs.Stores
{
    public class AddStoreAssetsDTO
    {
        public int AssetID { get; set; }
        [SerialNumberValidation]
        public string SerialNumber { get; set; }
        [PositiveCountValidation]
        public int Count { get; set; }
    }
}
