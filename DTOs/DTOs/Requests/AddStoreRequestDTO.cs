namespace DTOs.DTOs.Requests
{
    public class AddStoreRequestDTO
    {
        public int? WarehouseID { get; set; }    
        public string Note { get; set; }
        public List<AddStoreRequestAssetsDTO> Assets { get; set; }
    }
}
