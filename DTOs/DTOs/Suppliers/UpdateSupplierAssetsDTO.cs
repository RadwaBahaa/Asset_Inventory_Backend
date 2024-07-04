namespace DTOs.DTOs.Suppliers
{
    public class UpdateSupplierAssetsDTO
    {
        [PositiveCountValidation]
        public int? Count { get; set; }
    }
}
