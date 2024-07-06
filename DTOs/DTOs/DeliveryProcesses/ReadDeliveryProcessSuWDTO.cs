namespace DTOs.DTOs.DeliveryProcesses
{
    public class ReadDeliveryProcessSuWDTO
    {
        public int ProcessID { get; set; }      
        public int SupplierID { get; set; }     
        public int TotalAssets { get; set; }
        public DateTime DateTime { get; set; }
        public string FormattedDate => DateTime.ToString("yyyy-MM-dd HH:mm:ss");
        public List<ReadWarehouseProcessDTO> WarehouseProcesses { get; set; }
        public StageCompletionStep StageCompletionStep { get; set; }
    }
}
