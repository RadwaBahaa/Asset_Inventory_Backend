using DTOs.DTOs.Categories;

namespace DTOs.DTOs.Assets
{
    public class ReadAssetDTO
    {
        public int AssetID { get; set; }
        public string AssetName { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public ReadCategoryDTO Category { get; set; }
        public int SerialNumber { get; set; }
        public byte[] Picture { get; set; }   
    }
}
