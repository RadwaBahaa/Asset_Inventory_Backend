namespace DTOs.DTOs.Suppliers
{
    public class AddOrUpdateSupplierDTO
    {
        public string SupplierName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string? Address { get; set; }
    }
}
