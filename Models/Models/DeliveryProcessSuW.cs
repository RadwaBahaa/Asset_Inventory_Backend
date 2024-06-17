namespace Models.Models
{
    public class DeliveryProcessSuW
    {
        public int ProcessID { get; set; }      // Primary key
        public int SupplierID { get; set; }     // Forign Key from Supplier Entity
        public int TotalAssets { get; set; }
        public DateTime DateTime { get; set; }


        //RELATIONS
        public Supplier Supplier { get; set; }
        public List<AssetShipmentSuW> AssetShipmentSuW { get; set;}
        public List<WarehouseProcess> WarehouseProcesses { get; set; }
    }
}
