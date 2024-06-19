namespace DTOs.DTOs.Requests
{
    public class AddWarehouseRequestDTO
    {
        public int? SupplierID { get; set; }    
        public string Note { get; set; }
        public List<AddWarehouseRequestAssetsDTO> Assets { get; set; }
    }
}
