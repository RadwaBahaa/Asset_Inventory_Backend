namespace DTOs.DTOs.Warehouses
{
    public class UpdateWarehouseAssetsDTO
    {
        [SerialNumberValidation]
        public string? SerialNumber { get; set; }
        [PositiveCountValidation]
        public int? Count { get; set; }
    }
}
