namespace DTOs.DTOs.DeliveryProcesses
{
    public class ReadDeliveryProcessSuWDTO
    {
        public int ProcessID { get; set; }      
        public int SupplierID { get; set; }     
        public int TotalAssets { get; set; }
        public DateTime DateTime { get; set; }
        public List<ReadWarehouseProcessDTO> WarehouseProcesses { get; set; }
    }
}
