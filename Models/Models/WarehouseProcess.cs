namespace Models.Models
{
    public class WarehouseProcess
    {
        // ProcessID and WarehouseID are composit key
        public int ProcessID { get; set; }      // Forign key from DeliveryProcessSuW Entity
        public int WarehouseID { get; set; }    // Forign key from Warehouse Entity
        public int Quantity { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }


        //RELATIONS
        public DeliveryProcessSuW DeliveryProcessSuW { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
