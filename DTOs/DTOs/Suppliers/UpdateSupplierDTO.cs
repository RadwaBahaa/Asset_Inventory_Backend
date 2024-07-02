namespace DTOs.DTOs.Suppliers
{
    public class UpdateSupplierDTO
    {
        public string? SupplierName { get; set; }
        [LongitudeValidation]
        public double? Longitude { get; set; }
        [LatitudeValidation]
        public double? Latitude { get; set; }
        public string? Address { get; set; }
    }
}
