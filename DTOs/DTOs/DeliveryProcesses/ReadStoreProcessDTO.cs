namespace DTOs.DTOs.DeliveryProcesses
{
    public class ReadStoreProcessDTO
    {
        public int ProcessID { get; set; }
        public int StoreID { get; set; }  
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
    }
}
