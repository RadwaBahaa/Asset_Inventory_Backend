namespace Models.Models
{
    public class DeliveryProcessWSt
    {
        public int ProcessID { get; set; }      // Primary key
        public int? WarehouseID { get; set; }    // Forign key from Warehouse Entity
        public int TotalAssets { get; set; }
        public DateTime DateTime { get; set; }


        //RELATIONS
        public Warehouse Warehouse { get; set; }
        public List<StoreProcess> StoreProcesses { get; set; }
    }
}
