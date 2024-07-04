namespace DTOs.DTOs.DeliveryProcesses
{
    public class ReadDeliveryProcessWStDTO
    {
        public int ProcessID { get; set; }     
        public int WarehouseID { get; set; }   
        public int TotalAssets { get; set; }
        public DateTime DateTime { get; set; }
        public List<ReadStoreProcessDTO> StoreProcesses { get; set; }
    }
}
