namespace DTOs.DTOs.Suppliers
{
    public class ReadSupplierDTO
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int SRID { get; set; }
        public string Address { get; set; }
        public List<ReadSupplierAssetsDTO> SupplierAssets { get; set; }
    }
}
