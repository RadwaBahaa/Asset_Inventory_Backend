using NetTopologySuite.Geometries;

namespace Models.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }     // Primary key
        public string SupplierName { get; set; }
        public Point Location { get; set; }
        public string Address { get; set; }


        //RELATIONS
        public List<SupplierAsset> SupplierAssets { get; set; }
        public List<DeliveryProcessSuW> DeliveryProcessSuW {  get; set; }
        public List<WarehouseRequest> WarehouseRequests { get; set; }
    }
}
