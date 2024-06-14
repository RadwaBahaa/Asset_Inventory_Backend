namespace Models.Models
{
    public class WarehouseProcess
    {
        public int ProcessID { get; set; }
        public int WarehouseID { get; set; }
        public int Quantity { get; set; }

        //RELATIONS
        public DeliveryProcessSuW DeliveryProcessSuW { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
