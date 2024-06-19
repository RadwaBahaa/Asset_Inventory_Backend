namespace DTOs.DTOs.DeliveryProcesses
{
    public class ReadWarehouseProcessDTO
    {
        public int ProcessID { get; set; }      
        public int WarehouseID { get; set; }    
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
    }
}
