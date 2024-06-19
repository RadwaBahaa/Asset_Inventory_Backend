using NetTopologySuite.Geometries;

namespace DTOs.DTOs.Suppliers
{
    public class ReadSupplierDTO
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public Point Location { get; set; }
        public string Address { get; set; }
        public List<ReadSupplierAssetsDTO> Assets { get; set; }
    }
}
