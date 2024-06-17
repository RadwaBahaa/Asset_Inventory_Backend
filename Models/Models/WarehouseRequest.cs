using System.Security.Cryptography.X509Certificates;

namespace Models.Models
{
    public class WarehouseRequest
    {
        public int RequestID { get; set; }      // Primary key
        public int WarehouseID { get; set; }    // Forign key from Warehouse Entity
        public int SupplierID { get; set; }     // Forign key from Supplier Entity
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }


        //RELATIONS
        public Supplier Supplier { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<WarehouseRequestAsset> WarehouseRequestAsesets { get; set; }
    }
}
