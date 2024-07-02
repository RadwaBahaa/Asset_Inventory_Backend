namespace DTOs.DTOs.Suppliers
{
    public class UpdateSupplierAssetsDTO
    {
        [SerialNumberValidation]
        public string? SerialNumber { get; set; }
        [PositiveCountValidation]
        public int? Count { get; set; }
    }
}
