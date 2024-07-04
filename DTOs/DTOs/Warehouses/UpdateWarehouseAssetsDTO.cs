namespace DTOs.DTOs.Warehouses
{
    public class UpdateWarehouseAssetsDTO
    {
        [PositiveCountValidation]
        public int? Count { get; set; }
    }
}
