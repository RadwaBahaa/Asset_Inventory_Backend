namespace DTOs.DTOs.Stores
{
    public class AddStoreDTO
    {
        public string StoreName { get; set; }
        [LongitudeValidation]
        public double Longitude { get; set; }
        [LatitudeValidation]
        public double Latitude { get; set; }
        public string? Address { get; set; }
    }
}
