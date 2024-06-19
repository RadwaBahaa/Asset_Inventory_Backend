namespace DTOs.DTOs.Requests
{
    public class ReadStoreRequestDTO
    {
        public int RequestID { get; set; }      
        public int StoreID { get; set; }        
        public int WarehouseID { get; set; }    
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public List<ReadStoreRequestAssetsDTO> Assets { get; set; }
    }
}
