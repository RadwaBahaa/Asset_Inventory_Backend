namespace DTOs.DTOs.Stores
{
    public class UpdateStoreAssetsDTO
    {
        [SerialNumberValidation]
        public string? SerialNumber { get; set; }
        [PositiveCountValidation]
        public int? Count { get; set; }
    }
}
