namespace DTOs.DTOs.Stores
{
    public class UpdateStoreAssetsDTO
    {
        [PositiveCountValidation]
        public int? Count { get; set; }
    }
}
