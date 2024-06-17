namespace Models.Models
{
    public class StoreRequest
    {
        public int RequestID { get; set; }      // Primary key
        public int StoreID { get; set; }        // Forign key from Store Entity
        public int WarehouseID { get; set; }    // Forign key from Warehouse Entity
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }



        //RELATIONS
        public Store Store { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<StoreRequestAsset> StoreRequestAssests { get; set; }
    }
}
