using DTOs.Validation.Assets;

namespace DTOs.DTOs.Assets
{
    public class AddAssetDTO
    {
        public string AssetName { get; set; }
        public int CategoryID { get; set; }
        [PriceValidation]
        public float Price { get; set; }
        public string? Description { get; set; }
    }
}
