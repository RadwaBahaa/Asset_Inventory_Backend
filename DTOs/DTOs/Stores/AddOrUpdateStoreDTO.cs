namespace DTOs.DTOs.Stores
{
    public class AddOrUpdateStoreDTO
    {
        public string StoreName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string? Address { get; set; }
    }
}
