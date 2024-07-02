namespace DTOs.DTOs.Suppliers
{
    public class AddSupplierAssetsDTO
    {
        public int AssetID { get; set; }
        [SerialNumberValidation]
        public string SerialNumber { get; set; }
        [PositiveCountValidation]
        public int Count { get; set; }
    }
}
