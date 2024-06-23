namespace DTOs.DTOs.Stores
{
    public class ReadStoreDTO
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int SRID { get; set; }
        public string Address { get; set; }
        public List<ReadStoreAssetsDTO> StoreAssets { get; set; }
    }
}
