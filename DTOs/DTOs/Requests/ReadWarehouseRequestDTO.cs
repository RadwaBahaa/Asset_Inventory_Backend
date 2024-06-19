namespace DTOs.DTOs.Requests
{
    public class ReadWarehouseRequestDTO
    {
        public int RequestID { get; set; }     
        public int WarehouseID { get; set; }    
        public int SupplierID { get; set; }     
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public List<ReadWarehouseRequestAssetsDTO> Assets { get; set; }
    }
}
