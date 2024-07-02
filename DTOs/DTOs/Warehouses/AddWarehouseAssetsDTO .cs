namespace DTOs.DTOs.Warehouses

{
    public class AddWarehouseAssetsDTO
    {
        public int AssetID { get; set; }
        [SerialNumberValidation]
        public string SerialNumber { get; set; }
        [PositiveCountValidation]
        public int Count { get; set; }
    }
}
